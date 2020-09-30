using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Model;
using Newtonsoft.Json;
using System.Diagnostics;

namespace prototype
{
    public class Game
    {
        public int xSize = 23;       //Number of blocks left to right
        public int ySize = 19;       //Number of blocks top to bottom
        public int playerSize = 15;  //Player size in pixels e.g. 15x15

        public List<Player> players; //List of players with their stats
        public Map map;                    //Map data

        public Bitmap background, field, wallPic, playerPic;
        public bool gameStarted = false;
        public Game()
        {
            this.players = new List<Player>();
            this.map = new Map(xSize, ySize);
        }

        public void drawBackground()
        {
            Block[,] blocks = map.getBlocks();
            background = new Bitmap(25 * xSize, 25 * ySize);
            Color newColor = Color.Brown;
            wallPic = new Bitmap("wall.png");
            for (int x = 0; x < background.Width; x++)
            {
                for (int y = 0; y < background.Height; y++)
                {
                    background.SetPixel(x, y, newColor);
                }
            }

            //Draw walls
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (blocks[i, j] != null)
                    {
                        for (int x = 0; x < 25; x++)
                        {
                            for (int y = 0; y < 25; y++)
                            {
                                background.SetPixel(i * 25 + x, j * 25 + y, wallPic.GetPixel(x, y));
                            }
                        }
                    }
                }
            }
        }
        public void drawMap()
        {
            Block[,] blocks = map.getBlocks();
            field = getMap();
            wallPic = new Bitmap("wall.png");
            playerPic = new Bitmap("p1.png");
            //Draw players
            foreach (Player p in players)
            {
                int[] xy = p.getPos();
                for (int x = 0; x < playerSize; x++)
                {
                    for (int y = 0; y < playerSize; y++)
                    {
                        field.SetPixel(x + xy[0], y + xy[1], playerPic.GetPixel(x, y));
                    }
                }
            }
        }

        public Bitmap getMap()
        {
            RectangleF cloneRect = new RectangleF(0, 0, 25 * xSize, 25 * ySize);
            System.Drawing.Imaging.PixelFormat format = background.PixelFormat;
            return background.Clone(cloneRect, format);
        }
    }
}
