using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PlayerFedoraDecorator : PlayerDecorator
    {
        public PlayerFedoraDecorator(IPlayerStructure player) : base(player)
        {
             
        }
        public override string GetPlayerStructure()
        {
            return tempPlayer.GetPlayerStructure() + "1";
        }
    }
}
