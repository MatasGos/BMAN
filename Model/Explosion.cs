using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Explosion : RegularExplosive
    {
        public double removalTime { get; set; }
        public double explosionDuration { get; set; }

        public Explosion(int x, int y, double placeTime) : base(x, y)
        {
            explosionDuration = 2000.0;
            removalTime = placeTime + explosionDuration;
        }
    }
}
