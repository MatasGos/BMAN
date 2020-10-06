﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Model
{
    public class Map
    {
        //Size of the map in blocks e.g 15x15
        public int xSize { get; set; }
        public int ySize { get; set; }
        public Unit[,] units { get; set; }
        public Explosive[,] explosives { get; set; }

        public Map(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            units = new Unit[xSize, ySize];
        }

        public Unit[,] getUnits()
        {
            return units;
        }

        //Generates outter perimeter and also inner walls
        public void generateWalls()
        {
            Factory factory = new BlockFactory();
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (x == 0 || x == xSize - 1)
                    {
                        units[x, y] = factory.CreateBlock("Wall", x, y);
                    }
                    else if (y == 0 || y == ySize - 1)
                    {
                        units[x, y] = factory.CreateBlock("Wall", x, y);
                    }
                }
            }

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (x > 1 && y > 1)
                    {
                        if (x < (xSize - 1) && y < (ySize - 1))
                        {
                            if (x % 2 == 0 && y % 2 == 0)
                            {
                                if (x <= xSize/2)
                                {
                                    units[x, y] = factory.CreateBlock("Box", x, y);
                                }
                                else
                                {
                                    units[x, y] = factory.CreateBlock("Wall", x, y);
                                }
                            }
                        }
                    }
                }
            }
        }

        //MATAS LOOZERIS
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
                if(b[i] != null)
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

        public void PlaceExplosive(Player player)
        {
            if (player.action == "")
                return;

            int[] playerCenter = getCenterPlayer(new int[] { player.x, player.y });
            int[] playerTile = getTile(playerCenter[0], playerCenter[1]);
            Explosive toPlace = null;
            ExplosiveAbstractFactory factory;
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
                        toPlace = factory.CreateBomb(playerTile[0], playerTile[1]);
                        break;
                    case "placeMine":
                        toPlace = factory.CreateMine(playerTile[0], playerTile[1]);
                        break;
                    default:
                        break;
                }
                

                units[playerTile[0], playerTile[1]] = toPlace;
            }

            player.action = "";
        }
    }
}

