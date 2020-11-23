using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Explosive : Unit
    {
        public int explosionPower { get; set; }
        public double timeToExplosion { get; set; }

        private Player owner;

        public Explosive(int x, int y, Player owner) : base(x, y)
        {
            explosionPower = 2;
            timeToExplosion = 500.0;
            this.owner = owner;
        }

        public Player GetOwner()
        {
            return owner;
        }
    }
}
