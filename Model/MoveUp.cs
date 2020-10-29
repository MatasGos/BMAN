using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MoveUp : ICommand
    {
        public Player player { get; set; }
        public MoveUp(Player player)
        {
            this.player = player;
        }
        public void Execute()
        {
            this.player.directiony = -1;
        }

        public void Undo()
        {
            this.player.directiony = 1;
        }
    }
}
