using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ScoreboardTemplateProxy : IScoreboardTemplate
    {
        private IScoreboardTemplate ScoreboardTemplate;
        public ScoreboardTemplateProxy(IScoreboardTemplate ScoreboardTemplate)
        {
            this.ScoreboardTemplate = ScoreboardTemplate;
        }
        public List<Tuple<int, string, string>> FormTable()
        {
            if (ScoreboardTemplate == null)
            {
                throw new Exception();
            }
            return ScoreboardTemplate.FormTable();
        }

        public string Log()
        {
            var table = ScoreboardTemplate.FormTable();
            var sb = new System.Text.StringBuilder();
            sb.Append("--------------------\n");
            sb.Append(string.Format("|{0,10}|{1,7}|\n", "Name", "Score"));     
            foreach( var val in table)
            {
                sb.Append("--------------------\n");
                string temp = val.Item2;
                string[] oo = temp.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                sb.Append(string.Format("|{0,10}|{1,7}|\n", oo[0], oo[1]));
            }
            sb.Append("--------------------\n");
            return sb.ToString();
        }
    }
}
