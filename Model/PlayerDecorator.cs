using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Model
{
    public abstract class PlayerDecorator : IPlayerStructure
    {
        public IPlayerStructure tempPlayer;
        public PlayerDecorator(IPlayerStructure player)
        {
            tempPlayer = player;
        }
        public virtual string GetPlayerStructure()
        {
            return tempPlayer.GetPlayerStructure();
        }
    }
}
