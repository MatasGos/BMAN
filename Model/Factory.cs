using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Factory
    {
        public abstract Block CreateBlock(string type, int x, int y);
    }
}
