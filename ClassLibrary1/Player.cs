using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace prototype.Classes
{
    public class Player
    {
        private int x, y;
        private int speed;
        private int lives;
        private int bombTimer, bombStrength;
        //private Bitmap picture;
        private string name;

        public Player(int id, int speed, int[] xysize, string name)
        {
            switch (id)
            {
                case 1:
                    this.name = name;
                    this.x = 27;
                    this.y = 27;
                    this.speed = speed;
                    //this.picture = new Bitmap("p1.png");
                    this.lives = 5;
                    this.bombTimer = 200;
                    this.bombStrength = 2;
                    break;
                case 2:
                    this.name = name;
                    this.x = 25 * xysize[0] + xysize[0] * 2-25-27;
                    this.y = 27;
                    this.speed = speed;
                    //this.picture = new Bitmap("p1.png");
                    this.lives = 5;
                    this.bombTimer = 200;
                    this.bombStrength = 2;
                    break;
                case 3:
                    this.name = name;
                    this.x = 27;
                    this.y = 25 * xysize[1] + xysize[1] * 2 - 25 - 27;
                    this.speed = speed;
                    //this.picture = new Bitmap("p1.png");
                    this.lives = 5;
                    this.bombTimer = 200;
                    this.bombStrength = 2;
                    break;
                case 4:
                    this.name = name;
                    this.x = 25 * xysize[0] + xysize[0] * 2 - 25 - 27;
                    this.y = 25 * xysize[1] + xysize[1] * 2 - 25 - 27;
                    this.speed = speed;
                    //this.picture = new Bitmap("p1.png");
                    this.lives = 5;
                    this.bombTimer = 200;
                    this.bombStrength = 2;
                    break;
                default:
                    break;
            }
        }
        public int[] getPos()
        {
            return new int[] { this.x, this.y};
        }
        public void move(int x, int y)
        {
            this.x += x*speed;
            this.y += y*speed;
        }
        public void move1(int x, int y)
        {
            this.x += x;
            this.y += y;
        }
/*        public Color getPixel(int x,int y)
        {
            return picture.GetPixel(x, y);
        }*/
        public void reset()
        {
            this.x = 0;
            this.y = 0;
        }
        public int getPower()
        {
            return bombStrength;
        }
        public int bombTime()
        {
            return bombTimer;
        }
        public int getSpeed()
        {
            return speed;
        }

    }
}
