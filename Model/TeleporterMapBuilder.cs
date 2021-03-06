﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TeleporterMapBuilder : MapBuilder
    {
        private int xSize = 23;
        private int ySize = 19;
        private Map map;
        Factory factory;

        public TeleporterMapBuilder()
        {
            this.map = new Map(xSize, ySize);
            this.map.mapName = "concrete";
            factory = BlockFactorySingleton.GetInstance();
        }

        public override void BuildWalls()
        {
            BuildOutsideWalls();

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
                                
                                map.units[x, y] = factory.CreateBlock("Wall", x, y);
                            
                            }
                        }
                    }
                }
            }
        }

        private void BuildOutsideWalls()
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (x == 0 || x == xSize - 1)
                    {
                        map.units[x, y] = factory.CreateBlock("Wall", x, y);
                    }
                    else if (y == 0 || y == ySize - 1)
                    {
                        map.units[x, y] = factory.CreateBlock("Wall", x, y);
                    }
                }
            }
        }

        public override void BuildBox()
        {
            for (int x = 2; x < xSize-2; x++)
            {
                for (int y = 2; y < ySize-2; y++)
                {
                    if (map.units[x, y] == null)
                    {
                        map.units[x, y] = factory.CreateBlock("Box", x, y);
                    }
                } 
            }
        }

        public override void BuildTeleporter()
        {
            int[] teleporterIn = new int[] { 1, 4 };
            int[] teleporterOut = new int[] { 1, 10 };
            Unit input = factory.CreateBlock("Teleporter", teleporterIn[0], teleporterIn[1]);
            Unit output = factory.CreateBlock("Teleporter", teleporterOut[0], teleporterOut[1]);
            map.units[teleporterIn[0], teleporterIn[1]] = input;
            map.units[teleporterOut[0], teleporterOut[1]] = output;
            ((Teleporter)input).SetDestination((Teleporter)output);            
        }

        public override Map GetMap()
        {
            return map;
        }
    }
}
