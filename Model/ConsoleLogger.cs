using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(ILogMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
            type = "Receiver";
        }

        public override void receiveMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public override void sendMessage(string msg)
        {
            mediator.broadcastMessage(this, msg);
        }
    }
}
