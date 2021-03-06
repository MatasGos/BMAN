﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TeleporterChangeBoostAlgorithm : BoostAlgorithm
    {
        public override void UseBoost(Player player, Unit[,] units)
        {
            PlayerControlManager pcm = new PlayerControlManager(0, 0, null, null);
            int[] playerCenter = pcm.getCenterPlayer(new int[] { player.x, player.y });
            int[] playerTile = pcm.getTile(playerCenter[0], playerCenter[1]);
            Teleporter t = new Teleporter(playerTile[0], playerTile[1]);

            TeleporterChangeVisitor visitor = new TeleporterChangeVisitor(t);
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
