using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SuperMine : SuperExplosive
    {
        public SuperMine(int x, int y) : base(x, y)
        {
            isSolid = false;
            timeToExplosion = -1;
            explosionPower = 2;
        }
    }
}
