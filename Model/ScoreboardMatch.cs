using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public sealed class ScoreboardMatch : ScoreboardTemplate
    {

        protected override sealed IDictionary<string, string> TextStyle()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var score in playerScores)
            {
                if (score.Key.Equals(lastAdded))
                {
                    result.Add(score.Key, "bold");
                }
                else
                {
                    result.Add(score.Key, "");
                }
            }
            return result;
        }
        protected override sealed List<Tuple<int, string, string>> ToStringTable(IDictionary<string, string> styles)
        {
            List<Tuple<int, string, string>> table = new List<Tuple<int, string, string>>();
            foreach(var score in playerScores)
            {
                table.Add(new Tuple<int, string, string>(score.Value.Item1, 
                    String.Format("{0, -" + (longestNameLength + 1).ToString() + "} {1}", 
                    score.Key, score.Value.Item2), 
                    styles[score.Key]));
            }
            return table;
        }
    }
}
