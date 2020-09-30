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
using System.Runtime.InteropServices;

namespace prototype
{
    public class Game
    {
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;
        public byte[] Pixels;
        public int Depth;


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
            LockBits();
            
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
                                SetPixel(i * 25 + x, j * 25 + y, boxPicColor[x, y]);
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
                                SetPixel(i * 25 + x, j * 25 + y, bombPicColor[x, y]);
                            }
                        }
                    }
                }
            }
            //Draw players
            foreach (Player p in players)
            {
                int[] xy = p.getPos();
                for (int x = 0; x < playerSize; x++)
                {
                    for (int y = 0; y < playerSize; y++)
                    {
                        SetPixel(x + xy[0], y + xy[1], playerPicColor[x, y]);
                    }
                }
            }
            UnlockBits();
        }

        public Bitmap getMap()
        {
            RectangleF cloneRect = new RectangleF(0, 0, 25 * xSize, 25 * ySize);
            System.Drawing.Imaging.PixelFormat format = background.PixelFormat;
            return background.Clone(cloneRect, format);
        }

        public void LockBits()
        {
            try
            {
                // get total locked pixels count
                int PixelCount = xSize * ySize * 625;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, xSize, ySize);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize(field.PixelFormat);

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // Lock bitmap and return bitmap data
                bitmapData = field.LockBits(rect, ImageLockMode.ReadWrite,
                                             field.PixelFormat);

                // create byte array to copy pixel values
                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                // Unlock bitmap data
                field.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * xSize*25) + x) * cCount;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }
    }
}
