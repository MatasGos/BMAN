using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IScoreboardTemplate
    {
        public List<Tuple<int, string, string>> FormTable();
    }
}
