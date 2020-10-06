using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Unit
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool isSolid { get; set; }

        public Unit(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
