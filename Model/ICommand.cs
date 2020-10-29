using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    interface ICommand
    {
        public void Execute();

        public void Undo();
    }
}
