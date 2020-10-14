using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BombCountBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player)
        {
            player.bombCount += 1;
        }
    }
}
