using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// Chain of responsibility abstract class
    /// </summary>
    public abstract class MovementHandler
    {     
        public abstract void Calculate(Player player, int dx, int dy, int speed, PlayerControlManager pcm, Unit[] units);
        public abstract void SetNextChain(MovementHandler next);
        public abstract MovementHandler GetNextChain();
    }
}
