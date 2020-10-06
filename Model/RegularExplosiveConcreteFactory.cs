using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Model
{
    public class RegularExplosiveConcreteFactory : ExplosiveAbstractFactory
    {
        public override Explosive CreateBomb(int x, int y)
        {
            return new Bomb(x, y);
        }

        public override Explosive CreateExplosion(int x, int y, double placeTime)
        {
            return new Explosion(x, y, placeTime);
        }

        public override Explosive CreateMine(int x, int y)
        {
            return new Mine(x, y);
        }
    }
}
