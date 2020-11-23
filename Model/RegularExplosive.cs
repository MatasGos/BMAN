using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Model
{
    public abstract class RegularExplosive : Explosive
    {
        public RegularExplosive(int x, int y, Player owner) : base(x, y, owner)
        {
        }
    }
}
