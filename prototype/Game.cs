using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Model;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Bson;
using System.Drawing.Imaging;

namespace prototype
{
    public class Game
    {
        public int xSize = 23;       //Number of blocks left to right
        public int ySize = 19;       //Number of blocks top to bottom
        public int playerSize = 15;  //Player size in pixels e.g. 15x15

        public List<Player> players; //List of players with their stats
        public Map map;                    //Map data

        public Bitmap background, field;
        public int[] bits;
        Bitmap wallPic = new Bitmap("wall.png");
        Bitmap playerPic = new Bitmap("p1.png");
        Bitmap boxPic = new Bitmap("box.png");
        Bitmap bombPic = new Bitmap("bomb.jpg");

        Color[,] wallPicColor;
        Color[,] playerPicColor;
        Color[,] boxPicColor;
        Color[,] bombPicColor;
        public bool gameStarted = false;


        public Game()
        {
            this.players = new List<Player>();
            this.map = new Map(xSize, ySize);
            this.bits = new int[xSize * ySize];
        }
        public Color[,] GetPicColor(Bitmap originPic)
        {
            Color[,] destination = new Color[25, 25];
            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    destination[x,y] = originPic.GetPixel(x, y);
                }
            }
            return destination;
        }

        public Color[,] GetPlayerPicColor(Bitmap originPic)
        {
            Color[,] destination = new Color[25, 25];
            for (int x = 0; x < playerSize; x++)
            {
                for (int y = 0; y < playerSize; y++)
                {
                    destination[x, y] = originPic.GetPixel(x, y);
                }
            }
            return destination;
        }
        public void drawBackground()
        {
            Unit[,] blocks = map.getUnits();
            background = new Bitmap(25 * xSize, 25 * ySize);
            Color newColor = Color.BurlyWood;

            wallPicColor = GetPicColor(wallPic);
            playerPicColor = GetPlayerPicColor(playerPic);
            boxPicColor = GetPicColor(boxPic);
            bombPicColor = GetPicColor(bombPic);

            for (int x = 0; x < background.Width; x++)
            {
                for (int y = 0; y < background.Height; y++)
                {
                    background.SetPixel(x, y, newColor);
                    //bits[x+x*y] = newColor.ToArgb();
                }
            }

            //Draw walls
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (blocks[i, j] != null && blocks[i,j] is Wall)
                    {
                        for (int x = 0; x < 25; x++)
                        {
                            for (int y = 0; y < 25; y++)
                            {
                                background.SetPixel(i * 25 + x, j * 25 + y, wallPicColor[x, y]);
                                //bits[x + x * y] = wallPicColor[x, y].ToArgb();
                            }
                        }
                    }
                }
            }
          
        }
        public void drawMap()
        {
            Unit[,] blocks = map.getUnits();
            field = getMap();
            //Draw players
            foreach (Player p in players)
            {
                int[] xy = p.getPos();
                for (int x = 0; x < playerSize; x++)
                {
                    for (int y = 0; y < playerSize; y++)
                    {
                        field.SetPixel(x + xy[0], y + xy[1], playerPicColor[x, y]);
                    }
                }
            }
            //Draw boxes
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (blocks[i, j] != null && blocks[i, j] is Box)
                    {
                        for (int x = 0; x < 25; x++)
                        {
                            for (int y = 0; y < 25; y++)
                            {
                                background.SetPixel(i * 25 + x, j * 25 + y, boxPicColor[x, y]);
                            }
                        }
                    }
                }
            }
            //Draw bombs
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (blocks[i, j] != null && blocks[i, j] is Bomb)
                    {
                        for (int x = 0; x < 25; x++)
                        {
                            for (int y = 0; y < 25; y++)
                            {
                                background.SetPixel(i * 25 + x, j * 25 + y, bombPicColor[x, y]);
                            }
                        }
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
