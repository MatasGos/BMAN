using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IVisitor
    {
        public void Visit(Block block);
        public void Visit(Explosive explosive);
    }
}
