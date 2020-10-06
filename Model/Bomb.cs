using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Bomb : RegularExplosive
    {
        public Bomb(int x, int y) : base(x, y)
        {
            isSolid = false;
            timeToExplosion = 2000.0;
            explosionPower = 2;
        }
    }
}
