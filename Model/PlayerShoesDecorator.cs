using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PlayerShoesDecorator : PlayerDecorator
    {
        public PlayerShoesDecorator(IPlayerStructure player) : base(player)
        {
             
        }
        public override string GetPlayerStructure()
        {
            return tempPlayer.GetPlayerStructure() + "s";
        }
    }
}
