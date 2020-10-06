using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Mine : RegularExplosive
    {
        public Mine(int x, int y) : base (x, y)
        {
            isSolid = false;
            timeToExplosion = -1;
            explosionPower = 1;
        }
    }
}
