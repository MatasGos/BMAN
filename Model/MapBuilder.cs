using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class MapBuilder
    {
        public abstract void BuildWalls();

        public abstract void BuildBox();

        public abstract Map GetMap();

    }
}
