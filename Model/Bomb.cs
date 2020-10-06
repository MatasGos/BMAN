using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Bomb : RegularExplosive
    {
        public double detonationTime { get; set; }
        public Bomb(int x, int y, int explosionPower, double placeTime) : base(x, y)
        {
            isSolid = false;
            timeToExplosion = 2000.0;
            this.explosionPower = explosionPower;
            detonationTime = placeTime + timeToExplosion;
        }
    }
}
