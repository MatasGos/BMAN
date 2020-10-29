using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MoveRight : ICommand
    {
        public Player player { get; set; }
        public MoveRight(Player player)
        {
            this.player = player;
        }
        public void Execute()
        {
            this.player.directionx = 1;
        }

        public void Undo()
        {
            this.player.directionx = -1;
        }
    }
}
