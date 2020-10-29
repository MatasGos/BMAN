using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MoveUp : ICommand
    {
        private Player player;
        private MapFacade facade;
        private int speed;
        public MoveUp(Player player, MapFacade facade)
        {
            this.player = player;
            this.speed = player.speed;
            this.facade = facade;
        }
        public void Execute()
        {
            facade.Move(player, 0, -1, speed);
        }

        public void Undo()
        {
            facade.Move(player, 0, 1, speed);
        }
    }
}
