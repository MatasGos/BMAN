using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SuperExplosiveConcreteFactory : ExplosiveAbstractFactory
    {
        public override Explosive CreateBomb(int x, int y)
        {
            return new SuperBomb(x, y);
        }

        public override Explosive CreateExplosion(int x, int y, double placeTime)
        {
            //TODO: SUPER EXPLOSIVE
            return new Explosion(x, y, placeTime);
        }

        public override Explosive CreateMine(int x, int y)
        {
            return new SuperMine(x, y);
        }
    }
}
