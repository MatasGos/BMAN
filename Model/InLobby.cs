using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class InLobby : PlayerState
    {
        public bool IsAlive(Context context)
        {
            return false;
        }

        void PlayerState.Move(Context context)
        {
            throw new NotImplementedException();
        }

        void PlayerState.ReduceHealth(Context context)
        {
            throw new NotImplementedException();
        }

        void PlayerState.Undo(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
