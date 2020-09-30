using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Model
{
    public class Player
    {
        public string id { get; set; }
        public string username { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int speed { get; set; }

        public Player(string id, string username)
        {
            this.id = id;
            this.username = username;
            //DEFAULT VALUES
            this.x = 100;
            this.y = 100;
            this.speed = 3;
        }

        public int[] getPos()
        {
            return new int[] { this.x, this.y };
        }
    }
}
