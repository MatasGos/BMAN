﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ExplosionRangeBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player, Unit[,] units)
        {
            player.explosionPower += 1;
        }
    }
}
