using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MoveLeft : ICommand
    {
        public Player player { get; set; }
        public MoveLeft(Player player)
        {
            this.player = player;
        }
        public void Execute()
        {
            this.player.directionx = -1;
        }

        public void Undo()
        {
            this.player.directionx = 1;
        }
    }
}
