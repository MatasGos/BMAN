﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading.Tasks;

namespace Model
{
    public class Map
    {
        //Size of the map in blocks e.g 15x15
        public int xSize { get; set; }
        public int ySize { get; set; }
        public Block[,] blocks { get; set; }

        public Map(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            blocks = new Block[xSize, ySize];
        }

        public Block[,] getBlocks()
        {
            return blocks;
        }

        //Generates outter perimeter and also inner walls
        public void generateWalls()
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (x == 0 || x == xSize - 1)
                    {
                        blocks[x, y] = new Wall(x, y);
                    }
                    else if (y == 0 || y == ySize - 1)
                    {
                        blocks[x, y] = new Wall(x, y);
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
                                blocks[x, y] = new Wall(x, y);
                            }
                        }
                    }
                }
            }
        }

        public void Move(List<Player> playerList, string id, int x, int y)
        {
            Player movingPlayer = null;
            foreach (var player in playerList)
            {
                if (player.id == id)
                {
                    movingPlayer = player;
                    break;
                }
            }
            //tadas loxas
            movingPlayer.x += x;
            movingPlayer.y += y;
        }
    }
}
