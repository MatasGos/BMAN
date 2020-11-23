using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Explosion : RegularExplosive
    {
        public double removalTime { get; set; }
        public double explosionDuration { get; set; }

        public Explosion(int x, int y, double placeTime, Player owner) : base(x, y, owner)
        {
            explosionDuration = 2000.0;
            removalTime = placeTime + explosionDuration;
            isSolid = false;
        }
    }
}
