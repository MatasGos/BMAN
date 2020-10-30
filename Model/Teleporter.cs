using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Teleporter : Block
    {
        private Teleporter destination;
        public bool hasDestination;
        public Teleporter(int x, int y) : base(x, y)
        {
            isBreakable = false;
            isSolid = false;
            destination = null;
            hasDestination = false;
        }

        public void SetDestination(Teleporter destination)
        {
            this.destination = destination;
            hasDestination = true;
        }

        public Teleporter GetDestination()
        {
            return destination;
        }

        public bool HasDestination()
        {
            return hasDestination;
        }
    }
}
