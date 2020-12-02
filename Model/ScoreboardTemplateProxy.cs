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
    }
}
