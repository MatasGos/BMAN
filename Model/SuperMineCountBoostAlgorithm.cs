using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SuperMineCountBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player, Unit[,] units)
        {
            player.superMineCount += 1;
        }
    }
}
