using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Alive : PlayerState
    {
        public bool IsAlive(Context context)
        {
            return true;
        }

        void PlayerState.Move(Context context)
        {
            context.GetMovement().Move();
        }

        void PlayerState.ReduceHealth(Context context)
        {
            if (context.GetPlayer().health > 0)
                context.GetPlayer().health -= 1;

            if (context.GetPlayer().health <= 0)
            {
                context.GetPlayer().x = -25;
                context.GetPlayer().y = -25;
                context.SetState(new Dead());
            }
        }

        public void Undo(Context context)
        {
            context.GetMovement().Undo();           
        }
    }
}
