using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MapDirector
    {
        private MapBuilder mapBuilder;

        public MapDirector(MapBuilder mapBuilder)
        {
            this.mapBuilder = mapBuilder;
        }

        public Map getMap()
        {
            return this.mapBuilder.GetMap();
        }

        public void constructMap()
        {
            this.mapBuilder.BuildWalls();
            this.mapBuilder.BuildBox();
            this.mapBuilder.BuildTeleporter();
        }
    }
}
