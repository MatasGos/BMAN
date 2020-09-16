using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace prototype.Classes
{
    class Wall
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
