using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Teleporter : Block
    {
        private Teleporter destination;
        public Teleporter(int x, int y) : base(x, y)
        {
            isBreakable = false;
            isSolid = false;
            destination = null;
        }

        public void SetDestination(Teleporter destination)
        {
            this.destination = destination;
        }

        public bool HasDestination()
        {
            if (destination != null)
            {
                return true;
            }
            return false;
        }
    }
}
