using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Originator
    {
        private string state;
        public void setState(string state) {
            this.state = state;
        }
        public string getState()
        {
            return state;
        }

        public Memento SaveSateToMemento()
        {
            return new Memento(state);
        }
        public void GetStateFromMemento(Memento memento)
        {
            state = memento.GetState();
        }
    }
}
