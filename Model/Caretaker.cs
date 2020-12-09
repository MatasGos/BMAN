using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Caretaker
    {
        private List<Memento> mementoList = new List<Memento>();

        public void add(Memento state)
        {
            mementoList.Add(state);
        }
        public Memento Get(int index)
        {
            return mementoList[index];
        }
    }
}
