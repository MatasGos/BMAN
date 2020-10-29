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
    public class Game<A,B>
    {
        public int xSize = 23;      //Number of blocks(units) left to right in the map
        public int ySize = 19;      //Number of blocks(units) top to bottom in the map
        public int playerSize = 15; //Player size in pixels e.g. 15x15
        public int unitSize = 25;   //Unit(block) size in pixels e.g 25x25

        public List<Player> players;//List of players with their stats
        public Map map;             //Map data

        private GraphicsAdapter<A, B> field;
        private GraphicsAdapter<A, B> background;

        private GraphicsRepository<A, B> graphics;


        //Pictures saved as Color arrays
        B[,] wallPicColor;
        B[,] playerPicColor;
        B[,] boxPicColor;
        B[,] bombPicColor;
        B[,] minePicColor;
        B[,] boostPicColor;
        B[,] superbombPicColor;
        B[,] superminePicColor;
        B[,] explosionPicColor;
        B[,] fedoraColor;
        B[,] shoesColor;

        Dictionary<int, B[,]> playerPicsColors;

        public bool gameStarted = false;    //Bool showing if the game has started or ended/hasn't started yet

        public Game()
        {
            playerPicsColors = new Dictionary<int, B[,]>();
            this.players = new List<Player>();
            this.map = new Map(xSize, ySize);
            this.graphics = new GraphicsRepository<A, B>();
            this.field = graphics.CreateGraphicsObject(xSize * unitSize, ySize * unitSize);
        }

        public void FormPlayerImages()
        {
            B[,] tempColor = new B[playerSize, playerSize];
            B c = graphics.ColorFromArgb(0, 0, 0, 0);
            foreach (var p in players)
            {
                string s = p.pictureStructure;
                switch (s[0])
                {
                    case '0':
                        for (int x = 0; x < playerSize; x++)
                        {
                            for (int y = 0; y < playerSize; y++)
                            {
                                c = graphics.ColorFromArgb(graphics.ColorA(playerPicColor[x, y]), Math.Min(255, graphics.ColorR(playerPicColor[x, y]) + 255), graphics.ColorG(playerPicColor[x, y]), graphics.ColorB(playerPicColor[x, y]));
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                    case '1':
                        for (int x = 0; x < playerSize; x++)
                        {
                            for (int y = 0; y < playerSize; y++)
                            {
                                c = graphics.ColorFromArgb(graphics.ColorA(playerPicColor[x, y]), graphics.ColorR(playerPicColor[x, y]), graphics.ColorG(playerPicColor[x, y]), Math.Min(255, graphics.ColorB(playerPicColor[x, y]) + 255));
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                    case '2':
                        for (int x = 0; x < playerSize; x++)
                        {
                            for (int y = 0; y < playerSize; y++)
                            {
                                c = graphics.ColorFromArgb(graphics.ColorA(playerPicColor[x, y]), graphics.ColorR(playerPicColor[x, y]), Math.Min(255, graphics.ColorG(playerPicColor[x, y]) + 255), graphics.ColorB(playerPicColor[x, y]));
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                    case '3':
                        for (int x = 0; x < playerSize; x++)
                        {
                            for (int y = 0; y < playerSize; y++)
                            {
                                c = graphics.ColorFromArgb(graphics.ColorA(playerPicColor[x, y]), Math.Min(255, graphics.ColorR(playerPicColor[x, y]) + 255), Math.Min(255, graphics.ColorG(playerPicColor[x, y]) + 255), graphics.ColorB(playerPicColor[x, y]));
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                }
                if (s.Contains("f")) { 
                    for (int x = 0; x < playerSize; x++)
                    {
                        for (int y = 0; y < playerSize; y++)
                        {
                            if (graphics.ColorA(fedoraColor[x, y]) > 0)
                            {
                                c = graphics.ColorFromArgb(graphics.ColorA(fedoraColor[x, y]), graphics.ColorR(fedoraColor[x, y]), graphics.ColorG(fedoraColor[x, y]), graphics.ColorB(fedoraColor[x, y]));
                                tempColor[x, y] = c;
                            }
                        }
                    }
                }
                if (s.Contains("s"))
                {
                    for (int x = 0; x < playerSize; x++)
                        {
                            for (int y = 0; y < playerSize; y++)
                            {
                                if (graphics.ColorA(shoesColor[x, y]) > 0)
                                {
                                    c = graphics.ColorFromArgb(graphics.ColorA(shoesColor[x, y]), graphics.ColorR(shoesColor[x, y]), graphics.ColorG(shoesColor[x, y]), graphics.ColorB(shoesColor[x, y]));
                                    tempColor[x, y] = c;
                                }
                            }
                        }
                }
                B[,] temp = new B[playerSize, playerSize];
                for (int x = 0; x < playerSize; x++)
                {
                    for (int y = 0; y < playerSize; y++)
                    {
                        temp[x, y] = tempColor[x, y];
                    }
                }
                playerPicsColors.Add(int.Parse(s[0].ToString()), temp);
                //playerPicsColors[(int)s[0]] = tempColor;
                //string idk = s[0].toString()
                //playerPicsColors[int.Parse(s[0].toString())];

            }
        }

        public void drawBackground()
        {
            //Get map blocks(units) and initialise background picture with it's color
            Unit[,] blocks = map.getUnits();
            background = graphics.CreateGraphicsObject(xSize * unitSize, ySize * unitSize);
            B backgroundColor = graphics.GetBackgroundColor();

            //Save pictures as Color object arrays to know every pixel's color
            GraphicsAdapter<A, B> wallPic = graphics.CreateGraphicsObject(unitSize, unitSize);
            wallPic.SetImage(graphics.GetImage("wall"));
            wallPicColor = wallPic.GetColorArray();

            GraphicsAdapter<A, B> playerPic = graphics.CreateGraphicsObject(playerSize, playerSize);
            playerPic.SetImage(graphics.GetImage("p1"));
            playerPicColor = playerPic.GetColorArray();

            GraphicsAdapter<A, B> boxPic = graphics.CreateGraphicsObject(unitSize, unitSize);
            boxPic.SetImage(graphics.GetImage("box"));
            boxPicColor = boxPic.GetColorArray();

            GraphicsAdapter<A, B> bombPic = graphics.CreateGraphicsObject(unitSize, unitSize);
            bombPic.SetImage(graphics.GetImage("bomb"));
            bombPicColor = bombPic.GetColorArray();

            GraphicsAdapter<A, B> minePic = graphics.CreateGraphicsObject(unitSize, unitSize);
            minePic.SetImage(graphics.GetImage("mine"));
            minePicColor = minePic.GetColorArray();

            GraphicsAdapter<A, B> boostPic = graphics.CreateGraphicsObject(unitSize, unitSize);
            boostPic.SetImage(graphics.GetImage("mine"));
            boostPicColor = boostPic.GetColorArray();

            GraphicsAdapter<A, B> superbombPic = graphics.CreateGraphicsObject(unitSize, unitSize);
            superbombPic.SetImage(graphics.GetImage("superbomb"));
            superbombPicColor = superbombPic.GetColorArray();

            GraphicsAdapter<A, B> superminePic = graphics.CreateGraphicsObject(unitSize, unitSize);
            superminePic.SetImage(graphics.GetImage("supermine"));
            superminePicColor = superminePic.GetColorArray();

            GraphicsAdapter<A, B> explosionPic = graphics.CreateGraphicsObject(unitSize, unitSize);
            explosionPic.SetImage(graphics.GetImage("explosion"));
            explosionPicColor = explosionPic.GetColorArray();

            GraphicsAdapter<A, B> fedora = graphics.CreateGraphicsObject(playerSize, playerSize);
            fedora.SetImage(graphics.GetImage("fedora"));
            fedoraColor = fedora.GetColorArray();

            GraphicsAdapter<A, B> shoes = graphics.CreateGraphicsObject(playerSize, playerSize);
            shoes.SetImage(graphics.GetImage("shoes"));
            shoesColor = shoes.GetColorArray();

            FormPlayerImages();


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

            B[,] picColor = null;
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

        public GraphicsAdapter<A, B> GetField()
        {
            return field;
        }
    }
}
