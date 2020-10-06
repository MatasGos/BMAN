using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Explosive : Unit
    {
        public int explosionPower { get; set; }
        public double timeToExplosion { get; set; }

        public Explosive(int x, int y) : base(x, y)
        {
            explosionPower = 2;
            timeToExplosion = 500.0;
        }
    }
}
