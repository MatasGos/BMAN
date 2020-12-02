using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class QuarterMove : MovementHandler
    {
        private MovementHandler next;
        public override void Calculate(Player player, int dx, int dy, int speed, PlayerControlManager pcm, Unit[] units)
        {
            int multiplier = 4;

            int[,] edges = pcm.getEdges(new int[] { player.x, player.y });
            int x = (dx * speed) / multiplier;
            int y = (dy * speed) / multiplier;

            for (int i = 0; i < 4; i++)
            {
                edges[i, 0] += x;
                edges[i, 1] += y;
            }

            if (pcm.isOccupiedSquared(edges, units))
            {
                player.x += x;
                player.y += y;
            }
            else next.Calculate(player, dx, dy, speed, pcm, units);
            return ;
        }

        public override void SetNextChain(MovementHandler next)
        {
            this.next = next;
        }

        public override MovementHandler GetNextChain()
        {
            return next;
        }
    }
}
