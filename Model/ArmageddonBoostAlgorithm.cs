using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ArmageddonBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player, Unit[,] units)
        {
            ArmageddonVisitor visitor = new ArmageddonVisitor();
            for (int x = 0; x < units.GetLength(0); x++)
            {
                for (int y = 0; y < units.GetLength(1); y++)
                {
                    if (units[x, y] != null)
                    {
                        units[x, y].Accept(visitor);
                    }
                }
            }
        }
    }
}
