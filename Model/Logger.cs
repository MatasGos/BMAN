using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Logger
    {
        public ILogMediator mediator { get; set; }

        public string type { get; set; }

        public Logger(ILogMediator mediator)
        {
            this.mediator = mediator;
        }

        public abstract void sendMessage(string msg);

        public abstract void receiveMessage(string msg);
    }
}
