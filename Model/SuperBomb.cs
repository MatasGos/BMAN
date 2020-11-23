using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SuperBomb : SuperExplosive
    {
        public double detonationTime { get; set; }

        public SuperBomb(int x, int y, int explosionPower, double placeTime, Player owner) : base(x, y, owner)
        {
            isSolid = false;
            timeToExplosion = 3000.0;
            this.explosionPower = explosionPower;
            detonationTime = placeTime + timeToExplosion;
        }
    }
}
