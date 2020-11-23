using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SuperExplosiveConcreteFactory : ExplosiveAbstractFactory
    {
        public override Explosive CreateBomb(int x, int y, int explosionPower, double placeTime, Player owner)
        {
            return new SuperBomb(x, y, explosionPower, placeTime, owner);
        }

        public override Explosive CreateExplosion(int x, int y, double placeTime, Player owner)
        {
            return new SuperExplosion(x, y, placeTime, owner);
        }

        public override Explosive CreateMine(int x, int y, Player owner)
        {
            return new SuperMine(x, y, owner);
        }
    }
}
