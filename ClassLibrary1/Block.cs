using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class Block
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool isSolid { get; set; }

        public Block(int x, int y)
        {
            this.x = x;
            this.y = y;
            isSolid = true;
        }
        public int[] getPos()
        {
            return new int[] { this.x, this.y };
        }
    }
}
