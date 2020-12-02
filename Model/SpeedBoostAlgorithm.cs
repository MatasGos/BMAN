using System;
using System.Collections.Generic;
using System.Text;

namespace Model 
{
    public class SpeedBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player, Unit[,] units)
        {
            player.speed += 1;
        }
    }
}
