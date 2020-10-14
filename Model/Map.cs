using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Model
{
    public class Map : ICloneable<Map>
    {
        //Size of the map in blocks e.g 15x15
        public string mapName { get; set; }
        public int xSize { get; set; }
        public int ySize { get; set; }
        public Unit[,] units { get; set; }
        public Boost[,] boosts { get; set; }
        public const int width = 23;
        public const int height = 19;

        public Map()
        {
            this.xSize = width;
            this.ySize = height;
        }
        public Map(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            units = new Unit[xSize, ySize];
            boosts = new Boost[xSize, ySize];
        }

        public Unit[,] getUnits()
        {
            return units;
        }

        public Boost[,] getBoosts()
        {
            return boosts;
        }

        public void Move(Player movingPlayer)
        {
            int px = movingPlayer.directionx;
            int py = movingPlayer.directiony;

            if (px == 0 && py == 0)
            {
                return;
            }

            Unit[] b = getNearbyBlocks(movingPlayer.x, movingPlayer.y);
            int[,] edges = getEdges(new int[] { movingPlayer.x, movingPlayer.y });
            int[,] edges1 = getEdges(new int[] { movingPlayer.x, movingPlayer.y });

            for (int i = 0; i < 4; i++)
            {
                edges[i, 0] += px * movingPlayer.speed;
                edges[i, 1] += py * movingPlayer.speed;
                edges1[i, 0] += px;
                edges1[i, 1] += py;
            }

            if (isOccupiedSquared(edges, b))
            {
                movingPlayer.x += px * movingPlayer.speed;
                movingPlayer.y += py * movingPlayer.speed;
            }
            else if (isOccupiedSquared(edges1, b))
            {
                movingPlayer.x += px;
                movingPlayer.y += py;
            }

            movingPlayer.directionx = 0;
            movingPlayer.directiony = 0;
        }

        public bool isOccupiedSquared(int[,] edges, Unit[] b)
        {
            for (int i = 0; i < 4; i++)
            {
                if (isOccupied(new int[] { edges[i, 0], edges[i, 1] }, b))
                {
                    return false;
                }
            }
            return true;
        }

        public bool isOccupied(int[] xy, Unit[] b)
        {
            int[] topLeft = getTile(xy[0], xy[1]);

            for (int i = 0; i < 25; i++)
            {
                if (b[i] != null)
                {
                    if (b[i].x == topLeft[0] && b[i].y == topLeft[1] && b[i].isSolid == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int[,] getEdges(int[] xy)
        {
            int[,] edges = new int[4, 2];
            edges[0, 0] = xy[0];
            edges[0, 1] = xy[1];

            edges[1, 0] = xy[0] + 14;
            edges[1, 1] = xy[1];

            edges[2, 0] = xy[0];
            edges[2, 1] = xy[1] + 14;

            edges[3, 0] = xy[0] + 14;
            edges[3, 1] = xy[1] + 14;

            return edges;
        }

        public int[] getCenterPlayer(int[] xy)
        {
            int[] result = new int[2];
            result[0] = xy[0] + 15 / 2;
            result[1] = xy[1] + 15 / 2;
            return result;
        }

        public int[] getTile(int x, int y)
        {
            int[] result = new int[2];

            result[0] = x / 25;
            result[1] = y / 25;

            return result;
        }

        public Unit[] getNearbyBlocks(int posx, int posy)
        {
            Unit[] b = new Unit[25];
            int[] xy = getTile(posx, posy);
            int count = 0;
            for (int x = Math.Max(0, xy[0] - 2); x <= Math.Min(xy[0] + 2, xSize - 1); x++)
            {
                for (int y = Math.Max(0, xy[1] - 2); y <= Math.Min(xy[1] + 2, ySize - 1); y++)
                {
                    b[count++] = units[x, y];
                }
            }
            return b;
        }

        public void UpdateExplosives(double time)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    ExplosiveAbstractFactory factory;
                    if (units[x, y] is Explosion)
                    {
                        Explosion explosion = (Explosion)units[x, y];
                        if (explosion.removalTime < time)
                        {
                            units[x, y] = null;
                        }
                    }
                    else if (units[x, y] is Bomb)
                    {
                        factory = new RegularExplosiveConcreteFactory();
                        Bomb bomb = (Bomb)units[x, y];

                        if (bomb.detonationTime < time)
                        {
                            PlaceRegularExplosion(x, y, bomb.explosionPower, time, factory);
                        }
                    }
                    else if (units[x, y] is SuperBomb)
                    {
                        factory = new SuperExplosiveConcreteFactory();
                        SuperBomb bomb = (SuperBomb)units[x, y];

                        if (bomb.detonationTime < time)
                        {
                            PlaceSuperExplosion(x, y, bomb.explosionPower, time, factory);
                        }
                    }
                }
            }
        }

        public void PlaceRegularExplosion(int xTile, int yTile, int explosionPower, double placeTime, ExplosiveAbstractFactory factory)
        {
            bool topFinished = false;
            bool bottomFinished = false;
            bool leftFinished = false;
            bool rightFinished = false;

            units[xTile, yTile] = factory.CreateExplosion(xTile, yTile, placeTime);

            for (int i = 1; i < explosionPower; i++)
            {
                if (!topFinished)
                {
                    topFinished = RegularExplosionCreation(xTile, yTile - i, placeTime, topFinished, factory);
                }
                if (!bottomFinished)
                {
                    bottomFinished = RegularExplosionCreation(xTile, yTile + i, placeTime, bottomFinished, factory);
                }
                if (!leftFinished)
                {
                    leftFinished = RegularExplosionCreation(xTile - i, yTile, placeTime, leftFinished, factory);
                }
                if (!rightFinished)
                {
                    rightFinished = RegularExplosionCreation(xTile + i, yTile, placeTime, rightFinished, factory);
                }
            }
        }

        public bool RegularExplosionCreation(int x, int y, double placeTime, bool isFinished, ExplosiveAbstractFactory factory)
        {
            if (units[x, y] == null || units[x, y] is Explosion)
            {
                units[x, y] = factory.CreateExplosion(x, y, placeTime);
            }
            else
            {
                if (units[x, y] is Box)
                {
                    Random rand = new Random();
                    int n = rand.Next(100);
                    if (n < 30)
                    {
                        boosts[x, y] = PickBoostStrategy(x, y);
                    }
                    units[x, y] = factory.CreateExplosion(x, y, placeTime);

                }
                isFinished = true;
            }
            return isFinished;
        }

        public Boost PickBoostStrategy(int x, int y)
        {
            Boost boost = new Boost(x, y);
            Random rand = new Random();
            int n = rand.Next(100);
            if (n < 50)
            {
                boost.boostType = "speed";
                boost.algorithm = new SpeedBoostAlgorithm();
            }
            else if (n < 100)
            {
                boost.boostType = "explosion";
                boost.algorithm = new ExplosionRangeBoostAlgorithm();
            }
            return boost;
        }

        public void PlaceSuperExplosion(int xTile, int yTile, int explosionPower, double placeTime, ExplosiveAbstractFactory factory)
        {
            units[xTile, yTile] = factory.CreateExplosion(xTile, yTile, placeTime);

            for (int x = Math.Max(1, xTile - explosionPower); x <= Math.Min(xTile + explosionPower, xSize - 2); x++)
            {
                for (int y = Math.Max(1, yTile - explosionPower); y <= Math.Min(yTile + explosionPower, ySize - 2); y++)
                {
                    if (units[x, y] == null || units[x, y] is Box || units[x, y] is Explosion)
                    {
                        units[x, y] = factory.CreateExplosion(x, y, placeTime);
                    }
                }
            }
        }

        public void PlaceExplosive(Player player, double placeTime)
        {
            if (player.action == "") return;

            int[] playerCenter = getCenterPlayer(new int[] { player.x, player.y });
            int[] playerTile = getTile(playerCenter[0], playerCenter[1]);

            ExplosiveAbstractFactory factory;
            Explosive toPlace = null;

            if (units[playerTile[0], playerTile[1]] == null)
            {
                if (player.HasBoost("superexplosive"))
                {
                    factory = new SuperExplosiveConcreteFactory();
                }
                else
                {
                    factory = new RegularExplosiveConcreteFactory();
                }

                switch (player.action)
                {
                    case "placeBomb":
                        toPlace = factory.CreateBomb(playerTile[0], playerTile[1], player.explosionPower, placeTime);
                        break;
                    case "placeMine":
                        toPlace = factory.CreateMine(playerTile[0], playerTile[1]);
                        break;
                    default:
                        throw new Exception();
                }

                units[playerTile[0], playerTile[1]] = toPlace;
            }

            player.action = "";
        }
        public void PickupBoost(Player player)
        {
            int[] playerCenter = getCenterPlayer(new int[] { player.x, player.y });
            int[] playerTile = getTile(playerCenter[0], playerCenter[1]);

            if (boosts[playerTile[0], playerTile[1]] is Boost)
            {
                Boost boost = (Boost)boosts[playerTile[0], playerTile[1]];
                boost.algorithm.UseBoost(player);

                boosts[playerTile[0], playerTile[1]] = null;
            }
        }

        public Map Clone()
        {
            Map clone = (Map)this.MemberwiseClone();
            clone.units = new Unit[xSize, ySize];
            clone.boosts = new Boost[xSize, ySize];
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    clone.units[i, j] = units[i, j];
                    clone.boosts[i, j] = boosts[i, j];
                }
            }
            return clone;
        }
    }
}

