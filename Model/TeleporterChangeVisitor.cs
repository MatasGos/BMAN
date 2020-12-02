using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TeleporterChangeVisitor : IVisitor
    {
        private Teleporter Teleporter;

        public TeleporterChangeVisitor(Teleporter teleporter)
        {
            Teleporter = teleporter;
        }

        public void Visit(Block block)
        {
            if (block is Teleporter)
            {
                Teleporter t = (Teleporter)block;
                t.SetDestination(Teleporter);
            }
        }

        public void Visit(Explosive explosive)
        {
        }
    }
}
