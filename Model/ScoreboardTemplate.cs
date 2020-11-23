using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class ScoreboardTemplate
    {
        private List<Player> players;
        public IDictionary<string, ValueTuple<int, int, int>> playerScores;
        public int longestNameLength;

        public ScoreboardTemplate()
        {
            players = new List<Player>();
            playerScores = new Dictionary<string, ValueTuple<int, int, int>>();
            longestNameLength = 0;
        }
        
        public void AddPlayer(Player player)
        {
            players.Add(player);
            playerScores.Add(player.username, new ValueTuple<int, int, int>((int)player.num, 0, 0));
            if (player.username.Length > longestNameLength)
            {
                longestNameLength = player.username.Length;
            }
        }

        public void AddScore(Player player, int pointCount)
        {
            ValueTuple<int, int, int> tuple = playerScores[player.username];
            tuple.Item2 += pointCount;
            playerScores[player.username] = tuple;
        }

        public void ChangeStatus(Player player)
        {
            ValueTuple<int, int, int> tuple = playerScores[player.username];
            tuple.Item3 = 1 - tuple.Item3;
            playerScores[player.username] = tuple;
        }

        public abstract List<Tuple<int, string>> ToStringTable();
    }
}
