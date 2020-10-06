using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Block : Unit
    {
        public bool isBreakable { get; set; }

        public Block(int x, int y) : base(x, y)
        {
            isBreakable = true;
        }
    }
}
