using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MovementControl
    {
        private ICommand command;
        private LinkedList<ICommand> movementList;

        public MovementControl()
        {
            command = null;
            movementList = new LinkedList<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            this.command = command;
        }


        public void Move()
        {
            if (command != null)
            {
                command.Execute();
                if (movementList.Count > 20)
                {
                    movementList.RemoveLast();
                }
                movementList.AddFirst(command);
                this.command = null;
            }
        }

        public void Undo()
        {
            foreach (var item in movementList)
            {
                item.Undo();
            }
            movementList.Clear();
        }
    }
}
