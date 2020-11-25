using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Dead : PlayerState
    {
        public bool IsAlive(Context context)
        {
            return false;
        }

        public void Move(Context context)
        {
            throw new NotImplementedException();
        }

        public void ReduceHealth(Context context)
        {
            throw new NotImplementedException();
        }

        public void Undo(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
