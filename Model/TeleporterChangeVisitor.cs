using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TeleporterChangeVisitor : IVisitor
    {
        private Player Player;
        private Teleporter Teleporter;

        public TeleporterChangeVisitor(Player player, Teleporter teleporter)
        {
            Player = player;
            Teleporter = teleporter;
        }

        public void Visit(Block block)
        {
            if (block is Teleporter)
            {
                int[] playerCenter = pcm.getCenterPlayer(new int[] { Player.x, Player.y });
                int[] playerTile = pcm.getTile(playerCenter[0], playerCenter[1]);
                Teleporter t = new Teleporter(playerTile[0], playerTile[1]);

            }
        }

        public void Visit(Explosive explosive)
        {
        }
    }
}
