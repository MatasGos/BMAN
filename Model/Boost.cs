using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Boost : Block
    {
        public string boostType { get; set; }
        public BoostAlgorithm algorithm { get; set; }

        public Boost(int x, int y) : base(x, y)
        {
            isBreakable = false;
            isSolid = false;
        }
    }
}
