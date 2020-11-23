using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SuperMine : SuperExplosive
    {
        public SuperMine(int x, int y, Player owner) : base(x, y, owner)
        {
            isSolid = false;
            timeToExplosion = -1;
            explosionPower = 2;
        }
    }
}
