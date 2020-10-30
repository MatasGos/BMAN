using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SuperExplosiveConcreteFactory : ExplosiveAbstractFactory
    {
        public override Explosive CreateBomb(int x, int y, int explosionPower, double placeTime)
        {
            return new SuperBomb(x, y, explosionPower, placeTime);
        }

        public override Explosive CreateExplosion(int x, int y, double placeTime)
        {
            return new SuperExplosion(x, y, placeTime);
        }

        public override Explosive CreateMine(int x, int y)
        {
            return new SuperMine(x, y);
        }
    }
}
