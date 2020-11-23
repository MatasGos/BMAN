using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ScoreboardRound : ScoreboardTemplate
    {
        public override List<Tuple<int, string>> ToStringTable()
        {
            List<Tuple<int, string>> table = new List<Tuple<int, string>>();
            foreach(var score in playerScores)
            {
                if (score.Value.Item3 == 0)
                {
                    table.Add(new Tuple<int, string>(score.Value.Item1, String.Format("+{0, -" + (longestNameLength + 1).ToString() + "} {1}", score.Key, score.Value.Item2)));
                }
                else
                {
                    table.Add(new Tuple<int, string>(score.Value.Item1, String.Format("-{0, -" + (longestNameLength + 1).ToString() + "} {1}", score.Key, score.Value.Item2)));
                }
            }
            return table;
        }

    }
}
