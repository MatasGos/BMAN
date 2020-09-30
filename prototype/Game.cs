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
        const int xSize = 23;       //Number of blocks left to right
        const int ySize = 19;       //Number of blocks top to bottom
        const int playerSize = 15;  //Player size in pixels e.g. 15x15

        private LinkedList<Player> players; //List of players with their stats
        private Map map;                    //Map data

        private Bitmap background, wallPic;

        public Game()
        {
            this.players = new LinkedList<Player>();
            this.map = new Map(xSize, ySize);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(map, settings);
            Debug.WriteLine(json);
            Map map2 = JsonConvert.DeserializeObject<Map>(json, settings);
        }

        public void drawMap()
        {
            Block[,] blocks = map.getMap();
            this.background = new Bitmap(25 * xSize, 25 * ySize);
            this.wallPic = new Bitmap("wall.png");

            Color newColor = Color.Brown;
            for (int x = 0; x < background.Width; x++)
            {
                for (int y = 0; y < background.Height; y++)
                {
                    background.SetPixel(x, y, newColor);
                }
            }

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

        public Bitmap getMap()
        {
            RectangleF cloneRect = new RectangleF(0, 0, 25 * xSize, 25 * ySize);
            System.Drawing.Imaging.PixelFormat format = background.PixelFormat;
            return background.Clone(cloneRect, format);
        }
    }
}
