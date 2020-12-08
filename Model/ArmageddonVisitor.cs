using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ArmageddonVisitor : IVisitor
    {
        public void Visit(Block block)
        {
        }

        public void Visit(Explosive explosive)
        {
            if (explosive.explosionPower < 5)
            {
                explosive.explosionPower = 5;
            }
            explosive.timeToExplosion = 0.0;
        }
    }
}
