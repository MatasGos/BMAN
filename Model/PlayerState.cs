using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IPlayerState
    {
        public void Move(Context context);
        public void Undo(Context context);
        public void ReduceHealth(Context context);
        public bool IsAlive(Context context);
    }
}
