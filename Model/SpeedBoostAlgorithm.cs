using System;
using System.Collections.Generic;
using System.Text;

namespace Model 
{
    public class SpeedBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player)
        {
            player.speed += 5;
        }
    }
}
