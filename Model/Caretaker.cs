using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Caretaker
    {
        private List<Memento> mementoList = new List<Memento>();

        public int add(Memento state)
        {
            mementoList.Add(state);
            return mementoList.Count - 1;
        }
        public Memento Get(int index)
        {
            return mementoList[index];
        }
        public int GetLength()
        {
            return mementoList.Count;
        }
    }
}
