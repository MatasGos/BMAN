using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ConcreteMapBuilder : MapBuilder
    {
        private int xSize = 23;
        private int ySize = 19;
        private Map _map;
        public override void BuildWalls()
        {
            Factory factory = BlockFactorySingleton.GetInstance();

            BuildOutsideWalls(factory);

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (x > 1 && y > 1)
                    {
                        if (x < (xSize - 1) && y < (ySize - 1))
                        {
                            if (x % 2 == 0 && y % 2 == 0)
                            {
                                
                                _map.units[x, y] = factory.CreateBlock("Wall", x, y);
                            
                            }
                        }
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

        public ConcreteMapBuilder()
        {
            this._map = new Map(xSize, ySize);
        }
    }
}
