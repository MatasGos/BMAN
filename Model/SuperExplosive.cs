using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class SuperExplosive : Explosive
    {
        public SuperExplosive(int x, int y) : base(x, y)
        {
        }
    }
}
