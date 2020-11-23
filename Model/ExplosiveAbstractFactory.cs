using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class ExplosiveAbstractFactory
    {
        public abstract Explosive CreateBomb(int x, int y, int explosionPower, double placeTime, Player owner);

        public abstract Explosive CreateMine(int x, int y, Player owner);

        public abstract Explosive CreateExplosion(int x, int y, double placeTime, Player owner);
    }
}
