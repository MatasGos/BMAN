using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SuperBombCountBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player, Unit[,] units)
        {
            player.superBombCount += 1;
        }
    }
}
