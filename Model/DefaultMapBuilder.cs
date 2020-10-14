using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DefaultMapBuilder : MapBuilder
    {
        private int xSize = 23;
        private int ySize = 19;
        private Map _map;
        public override void BuildWalls()
        {
            Factory factory = BlockFactorySingleton.GetInstance();

            BuildOutsideWalls(factory);

            for (int x = 1; x < xSize-1; x++)
            {
                for (int y = 1; y < ySize-1; y++)
                {
                    if (x % 2 == 0 && y % 2 == 0)
                    { 
                        _map.units[x, y] = factory.CreateBlock("Wall", x, y);         
                    }
                }
            }
        }

        public override void BuildBox()
        {
            Factory factory = BlockFactorySingleton.GetInstance();

            for (int x = 2; x < xSize-2; x++)
            {
                for (int y = 2; y < ySize-2; y++)
                {
                    if (_map.units[x, y] == null)
                    {
                        _map.units[x, y] = factory.CreateBlock("Box", x, y);
                    }
                } 
            }
            for (int x = 3; x < xSize-3; x++)
            {
                _map.units[x, 1] = factory.CreateBlock("Box", x, 1);
                _map.units[x, ySize - 2] = factory.CreateBlock("Box", x, ySize-2);
            }

            for (int y = 3; y < ySize - 3; y++)
            {
                _map.units[1, y] = factory.CreateBlock("Box", 1, y);
                _map.units[xSize - 2, y] = factory.CreateBlock("Box", xSize-2, y);
            }
        }

        public override Map GetMap()
        {
            return _map;
        }
        private void BuildOutsideWalls(Factory factory)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (x == 0 || x == xSize - 1)
                    {
                        _map.units[x, y] = factory.CreateBlock("Wall", x, y);
                    }
                    else if (y == 0 || y == ySize - 1)
                    {
                        _map.units[x, y] = factory.CreateBlock("Wall", x, y);
                    }
                }
            }
        }

        public override void BuildTeleporter()
        {

        }

        public DefaultMapBuilder()
        {
            this._map = new Map(xSize, ySize);
        }
    }
}
