using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BoostRandomizeVisitor : IVisitor
    {
        public static Random rand = new Random();
        public void Visit(Block block)
        {
            if (block is Boost)
            {
                Boost boost = (Boost)block;
                int n = rand.Next(100);
                if (n < 25)
                {
                    boost.boostType = "speed";
                    boost.algorithm = new SpeedBoostAlgorithm();
                }
                else if (n >= 25 && n < 50)
                {
                    boost.boostType = "bomb";
                    boost.algorithm = new BombCountBoostAlgorithm();
                }
                else if (n >= 50 && n < 75)
                {
                    boost.boostType = "health";
                    boost.algorithm = new HealthBoostAlgorithm();
                }
                else if (n > 75)
                {
                    boost.boostType = "explosion";
                    boost.algorithm = new ExplosionRangeBoostAlgorithm();
                }
            }
        }

        public void Visit(Explosive explosive)
        {
        }
    }
}
