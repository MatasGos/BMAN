using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class PlayerBuilder
    {
        public abstract void BuildId(string id);

        public abstract void BuildUsername(string username);

        public abstract void BuildNum(int num);

        public abstract Player GetPlayer();

        
    }
}
