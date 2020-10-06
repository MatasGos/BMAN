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

        public override Explosive CreateExplosion(int x, int y)
        {
            throw new NotImplementedException();
        }

        public override Explosive CreateMine(int x, int y)
        {
            return new SuperMine(x, y);
        }
    }
}
