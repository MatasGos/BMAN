using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Unit
    {
        public int x;
        public int y;
        public bool isSolid;
        public Unit(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
