using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class ExplosiveAbstractFactory
    {
        public abstract Explosive CreateBomb(int x, int y);
        public abstract Explosive CreateMine(int x, int y);
        public abstract Explosive CreateExplosion(int x, int y);
    }
}
