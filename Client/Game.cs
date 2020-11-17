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
        private PictureFlyweight<A, B> pictures;

        Dictionary<int, B[,]> playerPicsColors;

        public bool gameStarted = false;    //Bool showing if the game has started or ended/hasn't started yet

        public Game()
        {
            playerPicsColors = new Dictionary<int, B[,]>();
            this.players = new List<Player>();
            this.map = new Map(xSize, ySize);
            this.graphics = new GraphicsRepository<A, B>();
            this.field = graphics.CreateGraphicsObject(xSize * unitSize, ySize * unitSize);
            this.pictures = new PictureFlyweight<A, B>();
        }

        public void FormPlayerImages()
        {
            playerPicsColors = new Dictionary<int, B[,]>();
            B[,] tempColor = new B[playerSize, playerSize];
            B c = graphics.ColorFromArgb(0, 0, 0, 0);
            B[,] playerPic = pictures.GetPictureArray("p1");
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
                                c = graphics.ColorFromArgb(graphics.ColorA(playerPic[x, y]), Math.Min(255, graphics.ColorR(playerPic[x, y]) + 255), graphics.ColorG(playerPic[x, y]), graphics.ColorB(playerPic[x, y]));
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                    case '1':
                        for (int x = 0; x < playerSize; x++)
                        {
                            for (int y = 0; y < playerSize; y++)
                            {
                                c = graphics.ColorFromArgb(graphics.ColorA(playerPic[x, y]), graphics.ColorR(playerPic[x, y]), graphics.ColorG(playerPic[x, y]), Math.Min(255, graphics.ColorB(playerPic[x, y]) + 255));
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                    case '2':
                        for (int x = 0; x < playerSize; x++)
                        {
                            for (int y = 0; y < playerSize; y++)
                            {
                                c = graphics.ColorFromArgb(graphics.ColorA(playerPic[x, y]), graphics.ColorR(playerPic[x, y]), Math.Min(255, graphics.ColorG(playerPic[x, y]) + 255), graphics.ColorB(playerPic[x, y]));
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                    case '3':
                        for (int x = 0; x < playerSize; x++)
                        {
                            for (int y = 0; y < playerSize; y++)
                            {
                                c = graphics.ColorFromArgb(graphics.ColorA(playerPic[x, y]), Math.Min(255, graphics.ColorR(playerPic[x, y]) + 255), Math.Min(255, graphics.ColorG(playerPic[x, y]) + 255), graphics.ColorB(playerPic[x, y]));
                                tempColor[x, y] = c;
                            }
                        }
                        break;
                }
                if (s.Contains("f")) {
                    B[,] fedoraPic = pictures.GetPictureArray("fedora");
                    for (int x = 0; x < playerSize; x++)
                    {
                        for (int y = 0; y < playerSize; y++)
                        {
                            if (graphics.ColorA(fedoraPic[x, y]) > 0)
                            {
                                c = graphics.ColorFromArgb(graphics.ColorA(fedoraPic[x, y]), graphics.ColorR(fedoraPic[x, y]), graphics.ColorG(fedoraPic[x, y]), graphics.ColorB(fedoraPic[x, y]));
                                tempColor[x, y] = c;
                            }
                        }
                    }
                }
                if (s.Contains("s"))
                {
                    B[,] shoesPic = pictures.GetPictureArray("shoes");
                    for (int x = 0; x < playerSize; x++)
                        {
                            for (int y = 0; y < playerSize; y++)
                            {
                                if (graphics.ColorA(shoesPic[x, y]) > 0)
                                {
                                    c = graphics.ColorFromArgb(graphics.ColorA(shoesPic[x, y]), graphics.ColorR(shoesPic[x, y]), graphics.ColorG(shoesPic[x, y]), graphics.ColorB(shoesPic[x, y]));
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
            background = graphics.CreateGraphicsObject(xSize * unitSize + 150, ySize * unitSize);
            B backgroundColor = graphics.GetBackgroundColor();

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
            B[,] wallPicTest = pictures.GetPictureArray("wall");
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
                                background.SetPixel(i * unitSize + x, j * unitSize + y, wallPicTest[x, y]);
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
            Explosive[,] explosions = map.getExplosions();
            field.SetImage(background.GetImageCopy());

            B[,] picColor = null;
            //Lock the Bitmap bits for faster drawing
            field.LockBits();

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
                                picColor = pictures.GetPictureArray("box");
                                break;
                            case Bomb x:
                                picColor = pictures.GetPictureArray("bomb");
                                break;
                            case Mine x:
                                picColor = pictures.GetPictureArray("mine");
                                break;
                            case SuperBomb x:
                                picColor = pictures.GetPictureArray("superbomb");
                                break;
                            case SuperMine x:
                                picColor = pictures.GetPictureArray("supermine");
                                break;
                            case Boost x:
                                picColor = pictures.GetPictureArray("boost");
                                break;
                            case Teleporter x:
                                if (x.HasDestination())
                                {
                                    picColor = pictures.GetPictureArray("teleporterin");
                                }
                                else
                                {
                                    picColor = pictures.GetPictureArray("teleporterout");
                                }
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

            //Draw explosions
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (explosions[i, j] != null)
                    {
                        switch (explosions[i, j])
                        {
                            case Explosion x:
                                picColor = pictures.GetPictureArray("explosion");
                                break;
                            case SuperExplosion x:
                                picColor = pictures.GetPictureArray("superexplosion");
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
