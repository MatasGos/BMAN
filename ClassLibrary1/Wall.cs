using System;
using System.Collections.Generic;
using System.Text;

namespace prototype.Classes
{
    public class Wall
    {
        private int x, y;
        public Wall(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int[] getPos()
        {
            return new int[] { this.x, this.y };
        }
    }
}
