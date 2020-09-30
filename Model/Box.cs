using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Box : Block
    {
        public Box(int x, int y) : base(x, y)
        {
            isBreakable = true;
            isSolid = true;
        }
    }
}
