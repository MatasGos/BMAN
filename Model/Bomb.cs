using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Bomb : Explosive
    {
        public Bomb(int x, int y) : base(x, y)
        {
            isSolid = false;
            timeToExplosion = 4000;
            explosionPower = 2;
        }
    }
}
