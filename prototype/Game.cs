using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Model;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace prototype
{
    public class Game
    {
        public int xSize = 23;      //Number of blocks(units) left to right in the map
        public int ySize = 19;      //Number of blocks(units) top to bottom in the map
        public int playerSize = 15; //Player size in pixels e.g. 15x15
        public int unitSize = 25;   //Unit(block) size in pixels e.g 25x25

        public List<Player> players;//List of players with their stats
        public Map map;             //Map data

        public Bitmap background, field;//Bitmap images used in showing the map on screen
        IntPtr Iptr = IntPtr.Zero;      //Pointer for bitmap data copying from memory
        BitmapData bitmapData;          //Map bitmap data construct with properties
        public byte[] pixels;           //Map pixel data array in bytes
        public int depth;               //How many bits per pixel are there in the map image

        //Pictures used in drawing the map
        Bitmap wallPic = new Bitmap("wall.png");
        Bitmap playerPic = new Bitmap("p1.png");
        Bitmap boxPic = new Bitmap("box.png");
        Bitmap bombPic = new Bitmap("bomb.jpg");
        Bitmap minePic = new Bitmap("mine.png");
        Bitmap superbombPic = new Bitmap("superbomb.png");
        Bitmap superminePic = new Bitmap("supermine.png");
        //Pictures saved as Color arrays
        Color[,] wallPicColor;
        Color[,] playerPicColor;
        Color[,] boxPicColor;
        Color[,] bombPicColor;
        Color[,] minePicColor;
        Color[,] superbombPicColor;
        Color[,] superminePicColor;

        public bool gameStarted = false;    //Bool showing if the game has started or ended/hasn't started yet

        public Game()
        {
            this.players = new List<Player>();
            this.map = new Map(xSize, ySize);
        }
        
        public void drawBackground()
        {
            //Get map blocks(units) and initialise background picture with it's color
            Unit[,] blocks = map.getUnits();
            background = new Bitmap(xSize * unitSize, ySize * unitSize);
            Color backgroundColor = Color.BurlyWood;

            //Save pictures as Color object arrays to know every pixel's color
            wallPicColor = GetPicColor(wallPic);
            playerPicColor = GetPicColor(playerPic);
            boxPicColor = GetPicColor(boxPic);
            bombPicColor = GetPicColor(bombPic);
            minePicColor = GetPicColor(minePic);
            superbombPicColor = GetPicColor(superbombPic);
            superminePicColor = GetPicColor(superminePic);

            //Draw background
            for (int x = 0; x < background.Width; x++)
            {
                for (int y = 0; y < background.Height; y++)
                {
                    background.SetPixel(x, y, backgroundColor);
                }
            }

            //Draw walls
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (blocks[i, j] != null && blocks[i,j] is Wall)
                    {
                        for (int x = 0; x < unitSize; x++)
                        {
                            for (int y = 0; y < unitSize; y++)
                            {
                                background.SetPixel(i * unitSize + x, j * unitSize + y, wallPicColor[x, y]);
                            }
                        }
                    }
                }
            }
          
        }

        public void drawMap()
        {
            //Get map data and get a background picture copy
            Unit[,] blocks = map.getUnits();
            field = getMap();


            Color[,] picColor = null;
            //Lock the Bitmap bits for faster drawing
            LockBits();

            //Draw units
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (blocks[i, j] != null && !(blocks[i,j] is Wall))
                    {
                        switch (blocks[i,j])
                        {
                            case Box x:
                                picColor = boxPicColor;
                                break;
                            case Bomb x:
                                picColor = bombPicColor;
                                break;
                            case Mine x:
                                picColor = minePicColor;
                                break;
                            case SuperBomb x:
                                picColor = superbombPicColor;
                                break;
                            case SuperMine x:
                                picColor = superminePicColor;
                                break;
                            default:
                                break;
                        }
                        for (int x = 0; x < unitSize; x++)
                        {
                            for (int y = 0; y < unitSize; y++)
                            {
                                SetPixel(i * unitSize + x, j * unitSize + y, picColor[x, y]);
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

            //Unlock Bitmap bits
            UnlockBits();
        }

        //Get map picture's copy as a bitmap
        public Bitmap getMap()
        {
            RectangleF cloneRectangle = new RectangleF(0, 0, xSize * unitSize, ySize * unitSize);
            PixelFormat format = background.PixelFormat;
            return background.Clone(cloneRectangle, format);
        }

        //Lock Bitmap for faster drawing
        public void LockBits()
        {
            try
            {
                //Total pixel count in map
                int pixelCount = xSize * unitSize * ySize * unitSize;

                //Rectangle for locking
                Rectangle rectangle = new Rectangle(0, 0, xSize * unitSize, ySize * unitSize);

                //Get source bitmap pixel format size
                depth = Bitmap.GetPixelFormatSize(field.PixelFormat);

                //Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (depth != 8 && depth != 24 && depth != 32)
                {
                    throw new Exception("Only 8, 24 and 32 bpp images are supported.");
                }

                //Lock bitmap and return bitmap data
                bitmapData = field.LockBits(rectangle, ImageLockMode.ReadWrite, field.PixelFormat);

                //Create byte array to copy pixel values
                int step = depth / 8;
                pixels = new byte[pixelCount * step];   //Galima iskelti kad kiekviena tick'a nekurtu naujo array/ jeigu lagins
                Iptr = bitmapData.Scan0;

                //Copy data from pointer to array
                Marshal.Copy(Iptr, pixels, 0, pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Unlock bitmap for faster drawing
        public void UnlockBits()
        {
            try
            {
                //Copy data from byte array to a pointer
                Marshal.Copy(pixels, 0, Iptr, pixels.Length);

                //Unlock bitmap data
                field.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get the picture's data as a Color array
        public Color[,] GetPicColor(Bitmap originPic)
        {
            Color[,] destination = new Color[originPic.Width, originPic.Height];
            for (int x = 0; x < originPic.Width; x++)
            {
                for (int y = 0; y < originPic.Height; y++)
                {
                    destination[x, y] = originPic.GetPixel(x, y);
                }
            }
            return destination;
        }

        //Set the pixel in a locked bitmap to a specific color
        public void SetPixel(int x, int y, Color color)
        {
            //Get color components count
            int cCount = depth / 8;

            //Get start index of the specified pixel
            int i = ((y * xSize * unitSize) + x) * cCount;

            if (depth == 32) //For 32 bpp set Red, Green, Blue and Alpha
            {
                pixels[i] = color.B;
                pixels[i + 1] = color.G;
                pixels[i + 2] = color.R;
                pixels[i + 3] = color.A;
            }
            if (depth == 24) //For 24 bpp set Red, Green and Blue
            {
                pixels[i] = color.B;
                pixels[i + 1] = color.G;
                pixels[i + 2] = color.R;
            }
            if (depth == 8) //For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                pixels[i] = color.B;
            }
        }
    }
}
