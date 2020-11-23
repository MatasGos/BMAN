using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Model
{
    public class RegularExplosiveConcreteFactory : ExplosiveAbstractFactory
    {
        public override Explosive CreateBomb(int x, int y, int explosionPower, double placeTime, Player owner)
        {
            return new Bomb(x, y, explosionPower, placeTime, owner);
        }

        public override Explosive CreateExplosion(int x, int y, double placeTime, Player owner)
        {
            return new Explosion(x, y, placeTime, owner);
        }

        public override Explosive CreateMine(int x, int y, Player owner)
        {
            return new Mine(x, y, owner);
        }
    }
}
