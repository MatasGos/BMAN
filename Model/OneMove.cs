using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OneMove : MovementHandler
    {
        private MovementHandler next;
        public override void Calculate(Player player, int dx, int dy, int speed, PlayerControlManager pcm, Unit[] units)
        {

            int[,] edges = pcm.getEdges(new int[] { player.x, player.y });

            for (int i = 0; i < 4; i++)
            {
                edges[i, 0] += dx;
                edges[i, 1] += dy;
            }

            if (pcm.isOccupiedSquared(edges, units))
            {
                player.x += dx;
                player.y += dy;
            }
            else return ;
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
