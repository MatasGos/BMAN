using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BombCountBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player, Unit[,] units)
        {
            player.bombCount += 1;
        }
    }
}
