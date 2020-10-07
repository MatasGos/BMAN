using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Boost : Block
    {
        public string boostType { get; set; }
        public BoostAlgorithm algorithm { get; set; }
        //public int timeLeft { get; set; } //in ticks?

        public Boost(int x, int y) : base(x, y)
        {
            isBreakable = false;
            isSolid = false;
            randBoost();
        }
        private void randBoost()
        {
            Random rand = new Random();
            int n = rand.Next(100);
            if (n < 50)
            {
                this.boostType = "speed";
                algorithm = new SpeedBoostAlgorithm();
            }
            else if (n < 100)
            {
                this.boostType = "explosion";
                algorithm = new ExplosionRangeBoostAlgorithm();
            }
        }
    }
}
