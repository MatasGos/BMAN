using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface ICommand
    {
        public void Execute();

        public void Undo();

    }
}
