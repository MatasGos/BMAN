using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace prototype.Classes
{
    public class Game
    {
        private Bitmap background, bombPic, wallPic;
        private LinkedList<Bomb> bombs;
        private int clientCount;
        private Map map;
        private const int playerSize = 15;
        const int xsize = 20;
        const int ysize = 20;
        public static readonly int[] backcolor = { 98, 65, 8 };
        private Bitmap[] playerPictures;

        //placeholder kol nera explosion klases
        const int explosionTime = 200;

        public Game()
        {
            clientCount = 0;
            map = new Map(xsize, ysize, backcolor);
            makeMap();
            getPlayerPics();
            this.bombPic = new Bitmap("bomb.jpg");
            this.wallPic = new Bitmap("wall.png");
        }

        public Bitmap getMap()
        {
            RectangleF cloneRect = new RectangleF(0, 0, 25 * xsize + xsize * 2, 25 * ysize + ysize * 2);
            System.Drawing.Imaging.PixelFormat format = background.PixelFormat;
            return background.Clone(cloneRect, format);
        }
        public void update()
        {
        }

        public void makeMap()
        {
            this.background = new Bitmap(25 * xsize + xsize * 2, 25 * ysize + ysize * 2);
            this.bombPic = new Bitmap("bomb.jpg");
            this.wallPic = new Bitmap("wall.png");

            Color black = Color.FromArgb(0, 0, 0);
            Color newColor = Color.FromArgb(backcolor[0], backcolor[1], backcolor[2]);
            for (int x = 0; x < background.Width; x++)
            {
                for (int y = 0; y < background.Height; y++)
                {
                    background.SetPixel(x, y, newColor);
                }
            }
            for (int x = 27; x < background.Width; x += 27)
            {
                for (int y = 0; y < background.Height; y++)
                {
                    background.SetPixel(x - 2, y, black);
                    background.SetPixel(x - 1, y, black);
                }
            }
            for (int y = 27; y < background.Height; y += 27)
            {
                for (int x = 0; x < background.Height; x++)
                {
                    background.SetPixel(x, y - 1, black);
                    background.SetPixel(x, y - 2, black);
                }
            }
            Wall[] walls = map.getWalls();
            for (int i = 0; i < map.getWallsCount(); i++)
            {
                int[] xy = walls[i].getPos();
                for (int x = 0; x < 25; x++)
                {
                    for (int y = 0; y < 25; y++)
                    {
                        background.SetPixel(x + xy[0], y + xy[1], wallPic.GetPixel(x, y));
                    }
                }
            }
        }
        public Bitmap getGame()
        {
            bombs = map.getBombs();
            Bitmap newMap = getMap();
            Player[] players = map.getPlayers();
            for (int i = 0; i < map.getPlayerCount(); i++)
            {
                Player player = players[i];
                int[] xy = player.getPos();
                for (int x = 0; x < playerSize; x++)
                {
                    for (int y = 0; y < playerSize; y++)
                    {
                        newMap.SetPixel(x + xy[0], y + xy[1], playerPictures[i].GetPixel(x, y));
                    }
                }
            }
            LinkedList<Bomb> bombsRemove = new LinkedList<Bomb>();
            foreach (Bomb bomb in bombs)
            {
                int tick = bomb.tick();
                if (tick > 0)
                {
                    int[] xy = bomb.getPos();
                    for (int x = 0; x < 25; x++)
                    {
                        for (int y = 0; y < 25; y++)
                        {
                            newMap.SetPixel(x + xy[0], y + xy[1], bombPic.GetPixel(x, y));
                        }
                    }
                }
                else if (tick > -explosionTime)
                {
                    int[] xy = bomb.getPos();
                    int power = bomb.getPower();
                    for (int x = 0 - 27 * power; x < 25 + 27 * power; x++)
                    {
                        //for (int x = Math.Max(0 - 27 * power, 0); x < Math.Min(25 + 27 * power, background.Width); x++)
                        if (x > 0 && x < 25)
                        {
                            for (int y = 0 - 27 * power; y < 25 + 27 * power; y++)
                            {
                                Color explosionColor = Color.FromArgb(230, 114, 56);
                                newMap.SetPixel(Math.Max(x + xy[0], 1), Math.Max(Math.Min(y + xy[1], background.Height - 1), 0), explosionColor);
                            }
                        }
                        else
                        {
                            for (int y = 0; y < 25; y++)
                            {
                                Color explosionColor = Color.FromArgb(230, 114, 56);
                                newMap.SetPixel(Math.Max(Math.Min(x + xy[0], background.Height - 1), 0), y + xy[1], explosionColor);
                            }
                        }
                    }
                }
                else
                {
                    bombsRemove.AddFirst(bomb);
                }
            }
            foreach (Bomb bomb in bombsRemove)
            {
                bombs.Remove(bomb);
            }
            return newMap;
        }
        public string Move(int id, int px, int py)
        {
            return map.Move(id, px, py);
        }
        public int join(string name)
        {
            map.addPlayer(name);
            return ++clientCount;
        }
        public void addBomb(int playerId)
        {
            map.addBomb(playerId);
        }
        public Player getPlayer(int id)
        {
            return map.getPlayers()[id];
        }
        private void getPlayerPics()
        {
            this.playerPictures = new Bitmap[4];
            for (int i = 0; i < 4; i++)
            {
                this.playerPictures[i] = new Bitmap("p1.png");
            }
        }
    }
}
