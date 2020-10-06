using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Boost : Block
    {
        public string boostType { get; set; }
        public int timeLeft { get; set; } //in ticks?

        public Boost(int x, int y, string boostType, int timeLeft) : base(x, y)
        {
            isBreakable = false;
            isSolid = false;
            this.boostType = boostType;
            this.timeLeft = timeLeft;
        }
    }
}
