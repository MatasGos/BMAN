using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PlayerList : Container
    {
        private List<Player> playerList;

        public PlayerList()
        {
            playerList = new List<Player>();
        }

        public Iterator getIterator()
        {
            return new PlayerIterator(playerList);
        }

        public void addPlayer(Player player)
        {
            playerList.Add(player);
        }

        public void removePlayer(Player player)
        {
            playerList.Remove(player);
        }

        public List<Player> getPlayers()
        {
            return playerList;
        }

        public int getCount()
        {
            return playerList.Count;
        }

    }
}
