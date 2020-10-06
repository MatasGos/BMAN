using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SuperBomb : SuperExplosive
    {
        public SuperBomb(int x, int y) : base(x, y)
        {
            isSolid = false;
            timeToExplosion = 3000;
            explosionPower = 3;
        }
    }
}
