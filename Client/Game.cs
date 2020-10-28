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
        //Bitmap playerPic = Images.p1;
        //Bitmap boxPic = Images.box;
        //Bitmap bombPic = Images.bomb;
        //Bitmap minePic = Images.mine;
        //Bitmap boostPic = Images.mine;
        //Bitmap superbombPic = Images.superbomb;
        //Bitmap superminePic = Images.supermine;
        //Bitmap explosionPic = Images.explosion;

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
        Color[,] fedoraColor;
        Color[,] shoesColor;

        Dictionary<int, Color[,]> playerPicsColors;

        public bool gameStarted = false;    //Bool showing if the game has started or ended/hasn't started yet

        public Game()
        {
            playerPicsColors = new Dictionary<int, Color[,]>();
            this.players = new List<Player>();
            this.map = new Map(xSize, ySize);
            this.field = new BitmapConcreteAdapter(xSize * unitSize, ySize * unitSize);
        }

        public void FormPlayerImages()
        {
            Color[,] tempColor = new Color[15, 15];
            Color c = Color.FromArgb(0, 0, 0, 0);
            foreach (var p in players)
            {
                string s = p.pictureStructure;

                //Color[,] tempColor;
                switch (s[0])
                {
                    case '0':
                        for (int x = 0; x < 15; x++)
                        {
                            for (int y = 0; y < 15; y++)
                            {
                                c = Color.FromArgb(playerPicColor[x, y].A, Math.Min(255, playerPicColor[x, y].R + 255), playerPicColor[x, y].G, playerPicColor[x, y].B);
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                    case '1':
                        for (int x = 0; x < 15; x++)
                        {
                            for (int y = 0; y < 15; y++)
                            {
                                c = Color.FromArgb(playerPicColor[x, y].A, playerPicColor[x, y].R, playerPicColor[x, y].G, Math.Min(255, playerPicColor[x, y].B + 255));
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                    case '2':
                        for (int x = 0; x < 15; x++)
                        {
                            for (int y = 0; y < 15; y++)
                            {
                                c = Color.FromArgb(playerPicColor[x, y].A, playerPicColor[x, y].R, Math.Min(255, playerPicColor[x, y].G + 255), playerPicColor[x, y].B);
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                    case '3':
                        for (int x = 0; x < 15; x++)
                        {
                            for (int y = 0; y < 15; y++)
                            {
                                c = Color.FromArgb(playerPicColor[x, y].A, Math.Min(255, playerPicColor[x, y].R + 255), Math.Min(255, playerPicColor[x, y].G + 255), playerPicColor[x, y].B);
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                }
                switch (s[1])
                {
                    case '0':
                        break;
                    case '1':
                        for (int x = 0; x < 15; x++)
                        {
                            for (int y = 0; y < 15; y++)
                            {
                                if (fedoraColor[x, y].A > 0)
                                {
                                    c = Color.FromArgb(fedoraColor[x, y].A, fedoraColor[x, y].R, fedoraColor[x, y].G, fedoraColor[x, y].B);
                                    tempColor[x, y] = c;
                                }
                            }
                        }
                        break;
                }
                switch (s[2])
                {
                    case '0':
                        break;
                    case '1':
                        for (int x = 0; x < 15; x++)
                        {
                            for (int y = 0; y < 15; y++)
                            {
                                if (shoesColor[x, y].A > 0)
                                {
                                    c = Color.FromArgb(shoesColor[x, y].A, shoesColor[x, y].R, shoesColor[x, y].G, shoesColor[x, y].B);
                                    tempColor[x, y] = c;
                                }
                            }
                        }
                        break;
                }
                //int.Parse(s[0].ToString())
                playerPicsColors.Add(int.Parse(s[0].ToString()), (Color[,])tempColor.Clone());
                //playerPicsColors[(int)s[0]] = tempColor;
                //string idk = s[0].toString()
                //playerPicsColors[int.Parse(s[0].toString())];

            }
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

            GraphicsAdapter<Bitmap, Color> fedora = new BitmapConcreteAdapter(playerSize, playerSize);
            fedora.SetImage(Images.fedora);
            fedoraColor = fedora.GetColorArray();

            GraphicsAdapter<Bitmap, Color> shoes = new BitmapConcreteAdapter(playerSize, playerSize);
            shoes.SetImage(Images.shoes);
            shoesColor = shoes.GetColorArray();

            GraphicsAdapter<Bitmap, Color> boxPic = new BitmapConcreteAdapter(unitSize, unitSize);
            boxPic.SetImage(Images.box);
            boxPicColor = boxPic.GetColorArray();

            FormPlayerImages();

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
                        field.SetPixel(x + xy[0], y + xy[1], playerPicsColors[(int)p.num][x, y]);
                    }
                }
            }

            //Unlock Bitmap bits
            field.UnlockBits();
        }

        public Bitmap GetImage()
        {
            return field.GetImage();
        }
    }
}
