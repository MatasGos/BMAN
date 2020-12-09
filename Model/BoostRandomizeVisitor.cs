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
                int n = rand.Next(135);
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
                else if (n >= 75 && n < 100)
                {
                    boost.boostType = "explosion";
                    boost.algorithm = new ExplosionRangeBoostAlgorithm();
                }
                else if (n >= 100 && n < 110)
                {
                    boost.boostType = "mine";
                    boost.algorithm = new MineCountBoostAlgorithm();
                }
                else if (n >= 110 && n < 115)
                {
                    boost.boostType = "supermine";
                    boost.algorithm = new SuperMineCountBoostAlgorithm();
                }
                else if (n >= 115 && n < 120)
                {
                    boost.boostType = "superbomb";
                    boost.algorithm = new SuperBombCountBoostAlgorithm();
                }
                else if (n >= 120 && n < 125)
                {
                    boost.boostType = "teleporter";
                    boost.algorithm = new TeleporterChangeBoostAlgorithm();
                }
                else if (n >= 125 && n < 130)
                {
                    boost.boostType = "boost";
                    boost.algorithm = new BoostRandomizeBoostAlgorithm();
                }
                else if (n >= 130 && n < 135)
                {
                    boost.boostType = "armageddon";
                    boost.algorithm = new ArmageddonBoostAlgorithm();
                }
            }
        }

        public void Visit(Explosive explosive)
        {
        }
    }
}
