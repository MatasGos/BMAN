using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Model;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Client
{
    //A - atvaizdo objektas (Bitmap)
    //B - spalvos objektas  (Color)
    public class Game
    {
        public int xSize = 23;      //Number of blocks(units) left to right in the map
        public int ySize = 19;      //Number of blocks(units) top to bottom in the map
        public int playerSize = 15; //Player size in pixels e.g. 15x15
        public int unitSize = 25;   //Unit(block) size in pixels e.g 25x25

        public List<Player> players;//List of players with their stats
        public Map map;             //Map data

        private GraphicsAdapter<Bitmap,Color> field;
        private GraphicsAdapter<Bitmap, Color> background;

        //Pictures used in drawing the map
        //Bitmap wallPic = Images.wall;
        ////Bitmap playerPic = Images.p1;
       // Bitmap boxPic = Images.box;
       // ////Bitmap bombPic = Images.bomb;
        //Bitmap minePic = Images.mine;
        //Bitmap boostPic = Images.mine;
       // Bitmap superbombPic = Images.superbomb;
        //Bitmap superminePic = Images.supermine;
      //  Bitmap explosionPic = Images.explosion;

        //Pictures saved as Color arrays
        Color[,] wallPicColor;
        Color[,] playerPicColor;
        Color[,] boxPicColor;
        Color[,] bombPicColor;
        Color[,] minePicColor;
        Color[,] boostPicColor;
        Color[,] superbombPicColor;
        Color[,] superminePicColor;
        Color[,] explosionPicColor;

        public bool gameStarted = false;    //Bool showing if the game has started or ended/hasn't started yet

        public Game()
        {
            this.players = new List<Player>();
            this.map = new Map(xSize, ySize);
            this.field = new BitmapConcreteAdapter(xSize * unitSize, ySize * unitSize);
        }
        
        public void drawBackground()
        {
            //Get map blocks(units) and initialise background picture with it's color
            Unit[,] blocks = map.getUnits();
            background = new BitmapConcreteAdapter(xSize * unitSize, ySize * unitSize);
            Color backgroundColor = Color.BurlyWood;

            //Save pictures as Color object arrays to know every pixel's color
            GraphicsAdapter<Bitmap, Color> wallPic = new BitmapConcreteAdapter(unitSize, unitSize);
            wallPic.SetImage(Images.wall);
            wallPicColor = wallPic.GetColorArray();
            GraphicsAdapter<Bitmap, Color> playerPic = new BitmapConcreteAdapter(playerSize, playerSize);
            playerPic.SetImage(Images.p1);
            playerPicColor = playerPic.GetColorArray();
            GraphicsAdapter<Bitmap, Color> boxPic = new BitmapConcreteAdapter(unitSize, unitSize);
            boxPic.SetImage(Images.box);
            boxPicColor = boxPic.GetColorArray();
            GraphicsAdapter<Bitmap, Color> bombPic = new BitmapConcreteAdapter(unitSize, unitSize);
            bombPic.SetImage(Images.bomb);
            bombPicColor = bombPic.GetColorArray();
            GraphicsAdapter<Bitmap, Color> minePic = new BitmapConcreteAdapter(unitSize, unitSize);
            minePic.SetImage(Images.mine);
            minePicColor = minePic.GetColorArray();
            GraphicsAdapter<Bitmap, Color> boostPic = new BitmapConcreteAdapter(unitSize, unitSize);
            boostPic.SetImage(Images.mine);
            boostPicColor = boostPic.GetColorArray();
            GraphicsAdapter<Bitmap, Color> superbombPic = new BitmapConcreteAdapter(unitSize, unitSize);
            superbombPic.SetImage(Images.superbomb);
            superbombPicColor = superbombPic.GetColorArray();
            GraphicsAdapter<Bitmap, Color> superminePic = new BitmapConcreteAdapter(unitSize, unitSize);
            superminePic.SetImage(Images.supermine);
            superminePicColor = superminePic.GetColorArray();
            GraphicsAdapter<Bitmap, Color> explosionPic = new BitmapConcreteAdapter(unitSize, unitSize);
            explosionPic.SetImage(Images.explosion);
            explosionPicColor = explosionPic.GetColorArray();

            background.LockBits();
            //Draw background
            for (int x = 0; x < background.GetWidth(); x++)
            {
                for (int y = 0; y < background.GetHeight(); y++)
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
            background.UnlockBits();
        }

        public void drawMap()
        {
            //Get map data and get a background picture copy
            Unit[,] blocks = map.getUnits();
            Boost[,] boosts = map.getBoosts();
            field.SetImage(background.GetImageCopy());

            Color[,] picColor = null;
            //Lock the Bitmap bits for faster drawing
            field.LockBits();

            //Draw boosts
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (boosts[i, j] != null)
                    {
                        //TODO: Switch thru different boost pictures
                        picColor = boostPicColor;

                        for (int x = 0; x < unitSize; x++)
                        {
                            for (int y = 0; y < unitSize; y++)
                            {
                                field.SetPixel(i * unitSize + x, j * unitSize + y, picColor[x, y]);
                            }
                        }
                    }
                }
            }

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
                            case Explosion x:
                                picColor = explosionPicColor;
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
                                field.SetPixel(i * unitSize + x, j * unitSize + y, picColor[x, y]);
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
                        field.SetPixel(x + xy[0], y + xy[1], playerPicColor[x, y]);
                    }
                }
            }

            //Unlock Bitmap bits
            field.UnlockBits();
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

        public Bitmap GetImage()
        {
            return field.GetImage();
        }
    }
}
