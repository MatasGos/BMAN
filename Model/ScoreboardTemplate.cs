using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class ScoreboardTemplate : IScoreboardTemplate
    {
        protected List<Player> players;
        public IDictionary<string, ValueTuple<int, int, int>> playerScores;
        public int longestNameLength;
        public string lastAdded;

        public ScoreboardTemplate()
        {
            players = new List<Player>();
            playerScores = new Dictionary<string, ValueTuple<int, int, int>>();
            longestNameLength = 0;
            lastAdded = "";
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
            lastAdded = player.username;
        }

        public void ChangeStatus(Player player)
        {
            ValueTuple<int, int, int> tuple = playerScores[player.username];
            tuple.Item3 = 1 - tuple.Item3;
            playerScores[player.username] = tuple;
        }

        protected abstract List<Tuple<int, string, string>> ToStringTable(IDictionary<string, string> styles);

        protected abstract IDictionary<string, string> TextStyle();

        public List<Tuple<int, string, string>> FormTable()
        {
            IDictionary<string, string> styles = TextStyle();
            List<Tuple<int, string, string>> table = ToStringTable(styles);
            return table;
        }

    }
}
