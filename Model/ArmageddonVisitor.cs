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
            if (explosive is Bomb)
            {
                Bomb b = (Bomb)explosive;
                b.detonationTime = 0.0;
            }
            else if (explosive is SuperBomb)
            {
                SuperBomb sb = (SuperBomb)explosive;
                sb.detonationTime = 0.0;
            }
        }
    }
}
