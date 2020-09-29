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
        private string id;
        private string username;
        public Player(string _id, int x, int y)
        {
            this.username = _id;
            this.id = _id;
            this.x = x;
            this.y = y;
        }
        public Player(int no, int speed, int[] xysize, string name, string id)
        {
            switch (no)
            {
                case 1:
                    this.id = id;
                    this.username = name;
                    this.x = 27;
                    this.y = 27;
                    this.speed = speed;
                    this.lives = 5;
                    this.bombTimer = 200;
                    this.bombStrength = 2;
                    break;
                case 2:
                    this.id = id;
                    this.username = name;
                    this.x = 25 * xysize[0] + xysize[0] * 2-25-27;
                    this.y = 27;
                    this.speed = speed;
                    this.lives = 5;
                    this.bombTimer = 200;
                    this.bombStrength = 2;
                    break;
                case 3:
                    this.id = id;
                    this.username = name;
                    this.x = 27;
                    this.y = 25 * xysize[1] + xysize[1] * 2 - 25 - 27;
                    this.speed = speed;
                    this.lives = 5;
                    this.bombTimer = 200;
                    this.bombStrength = 2;
                    break;
                case 4:
                    this.id = id;
                    this.username = name;
                    this.x = 25 * xysize[0] + xysize[0] * 2 - 25 - 27;
                    this.y = 25 * xysize[1] + xysize[1] * 2 - 25 - 27;
                    this.speed = speed;
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
        public string getString()
        {
            return this.id + "+" + this.x + "+" + this.y;
        }
        public string getId()
        {
            return this.id;
        }

    }
}
