using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Wall : Block
    {
        public Wall(int x, int y) : base(x, y)
        {
            isBreakable = false;
        }
    }
}
