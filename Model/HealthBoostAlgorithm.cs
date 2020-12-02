using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class HealthBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player, Unit[,] units)
        {
            player.health += 1;
        }
    }
}
