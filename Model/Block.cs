using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Block
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool isBreakable { get; set; }

        public Block(int x, int y)
        {
            this.x = x;
            this.y = y;
            isBreakable = true;
        }
        public int[] getPos()
        {
            return new int[] { this.x, this.y };
        }
    }
}
