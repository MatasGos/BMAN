using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace prototype.Classes
{
    public class Map
    {
        private int xSize, ySize;   //Size of the map in blocks e.g 15x15
        private Block[,] blocks;    //Mape made out of blocks
        private int playerSize = 15;

        public Map(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            blocks = new Block[xSize, ySize];
            generateWalls();
            //Debug.WriteLine(printWalls());
        }

        public Block[,] getMap()
        {
            return blocks;
        }

        public string printWalls()
        {
            StringBuilder str = new StringBuilder();
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (blocks[x, y] == null)
                    {
                        str.Append("o");
                    }
                    else
                    {
                        str.Append("X");
                    }
                }
                str.Append("\n");
            }
            return str.ToString();
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
    }
}

