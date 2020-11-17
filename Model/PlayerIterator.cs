using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class PlayerIterator : Iterator
    {
        private List<Player> playerList;
        private int index;

        public PlayerIterator(List<Player> pList)
        {
            playerList = pList;
        }

        public bool hasNext()
        {
            if (index < playerList.Count)
            {
                return true;
            }
            return false;
        }

        public object next()
        {
            if (this.hasNext())
            {
                return playerList[index++];
            }
            return null;
        }

    }
}
