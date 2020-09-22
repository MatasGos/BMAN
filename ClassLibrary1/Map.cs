using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;


namespace prototype.Classes
{
    public class Map
    {
        private int xsize, ysize;
        private Player[] players;
        private int playerCount;
        private int defaultSpeed = 1;
        private int clientCount;
        private int playerLimit = 4;
        private LinkedList<Bomb> bombs;
        private Wall[] walls;
        private int wallCount;
        private int playerSize = 15;
        public Map(int xsize, int ysize, int[] color)
        {
            this.xsize = xsize;
            this.ysize = ysize;
            drawMap(color);
            this.clientCount = 0;
        }
        public void drawMap(int[] color)
        {

            this.wallCount = 0;
            plantWalls(wallCount);
            playerCount = 0;
            players = new Player[10];
            bombs = new LinkedList<Bomb>();
        }
        public void addPlayer(string name)
        {
            if (playerCount < playerLimit)
            {
                players[playerCount] = new Player(playerCount + 1, defaultSpeed, new int[] { this.xsize, this.ysize }, name);
                this.playerCount = playerCount + 1;
            }
            else
            {
            }
        }

        public int[] getTile(int x, int y)
        {
            int[] result = new int[2];

            result[0] = x / 27 * 27;
            result[1] = y / 27 * 27;

            return result;
        }
        public Player[] getPlayers()
        {
            return players;
        }
        public int getPlayerCount()
        {
            return playerCount;
        }
        public int join(string name)
        {
            this.addPlayer(name);
            return ++clientCount;
        }
        public string Move(int id, int px, int py)
        {
            int[] xy = players[id - 1].getPos();
            int step = players[id - 1].getSpeed();
            int[] center = getCenterPlayer(xy);
            int x = center[0] + px * playerSize / 2;
            int y = center[1] + py * playerSize / 2;
            center[0] += px * (step + playerSize / 2);
            center[1] += py * (step + playerSize / 2);
            int[,] edges = getEdges(xy);
            int[,] edges1 = getEdges(xy);
            for (int i = 0; i < 4; i++)
            {
                edges[i, 0] += px*step;
                edges[i, 1] += py*step;
                edges1[i, 0] += px;
                edges1[i, 1] += py;
            }
            if (!isOccupied(center) && isOccupiedSquared(edges))
            {
                players[id - 1].move(px, py);
            }
            else if (!isOccupied(new int[] { x, y }) && isOccupiedSquared(edges1))
            {
                players[id - 1].move1(px, py);
            }
            string ret = edges[0, 0] + "-" + edges[0, 1] + "|||" + edges[1, 0] + "-" + edges[1, 1] + "|||" + edges[2, 0] + "-" + edges[2, 1] + "|||" + edges[3, 0] + "-" + edges[3, 1];
            return ret;
        }
        public bool isOccupiedSquared(int[,] edges)
        {
            for (int i = 0; i < 4; i++)
            {
                if (isOccupied(new int[] { edges[i,0], edges[i,1] }))
                {
                    return false;
                }
            }
            return true;
        }
        public int[] test(int id, int px, int py)
        {
            int[] xy = players[id - 1].getPos();
            int step = players[id - 1].getSpeed();
            int[] center = getCenterPlayer(xy);
            center[0] += px * step;
            center[1] += py * step;
            return center;
        }
        public bool isOccupied(int[] xy)
        {
            int[] topLeft = getTile(xy[0], xy[1]);

            for (int i = 0; i < wallCount; i++)
            {
                int[] temp = walls[i].getPos();
                if (temp[0] == topLeft[0] && temp[1] == topLeft[1])
                {
                    return true;
                }
            }
            if (bombs.Contains(new Bomb(topLeft[0], topLeft[1])))
            {
                return !bombs.Find(new Bomb(topLeft[0], topLeft[1])).Value.getWT();
            }

            return false;
        }
        public int[,] getEdges(int[] xy)
        {
            int[,] edges = new int[4, 2];
            edges[0, 0] = xy[0];
            edges[0, 1] = xy[1];

            edges[1, 0] = xy[0] + playerSize;
            edges[1, 1] = xy[1];

            edges[2, 0] = xy[0];
            edges[2, 1] = xy[1] + playerSize;

            edges[3, 0] = xy[0] + playerSize;
            edges[3, 1] = xy[1] + playerSize;

            return edges;
        }
        public int[] getCenterPlayer(int[] xy)
        {
            int[] result = new int[2];
            result[0] = xy[0] + playerSize / 2;
            result[1] = xy[1] + playerSize / 2;
            return result;    
        }
        public void addBomb(int playerId)
        {
           // int[] playerpos = players[playerId - 1].getPos();
            int [] playerpos = getCenterPlayer(players[playerId - 1].getPos());
            bombs.AddFirst(new Bomb(players[playerId-1], this.getTile(playerpos[0], playerpos[1])));
        }
        private void plantWalls(int wallCount)
        {
            //border walls
            walls = new Wall[wallCount+2*xsize+2*ysize-4];

            var rand = new Random();
            for (int i = 0; i < wallCount; i++)
            {
                int x = rand.Next(2, this.xsize-2);
                int y = rand.Next(2, this.ysize-2);
                if(walls.Contains(new Wall(x*27, y*27)))
                {
                    i--;
                }
                else
                {
                    walls[i] = new Wall(x*27, y*27);
                }
            }
            for (int x = 0; x < this.xsize; x++)
            {
                walls[this.wallCount++] = new Wall(x * 27, 0);
                walls[this.wallCount++] = new Wall(x * 27,  (ysize-1)*27);
            }
            for (int y = 1; y < this.ysize - 1; y++)
            {
                walls[this.wallCount++] = new Wall(0, y * 27);
                walls[this.wallCount++] = new Wall((xsize-1) * 27, y * 27);
            }
        }
        public LinkedList<Bomb> getBombs()
        {
            return bombs;
        }
        public Wall[] getWalls()
        {
            return walls;
        }
        public int getWallsCount()
        {
            return wallCount;
        }
    }
}

