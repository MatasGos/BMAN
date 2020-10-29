using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MoveDown : ICommand
    {
        public Player player { get; set; }
        public MoveDown(Player player)
        {
            this.player = player;
        }
        public void Execute()
        {
            this.player.directiony = 1;
        }

        public void Undo()
        {
            this.player.directiony = -1;
        }
    }
}
