using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MineCountBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player, Unit[,] units)
        {
            player.mineCount += 1;
        }
    }
}
