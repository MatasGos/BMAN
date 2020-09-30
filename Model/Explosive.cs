using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Explosive : Unit
    {
        public int x;
        public int y;
        public int timeToExplosion; //miliseconds
        public int explosionPower;
        public Explosive(int x, int y) : base(x, y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
