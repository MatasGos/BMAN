using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LogPlayer : Logger
    {
        public LogPlayer(ILogMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
            type = "Sender";
        }

        public override void receiveMessage(string msg)
        {
            Console.WriteLine($"LOGPLAYER: { msg }");
        }

        public override void sendMessage(string msg)
        {
            mediator.broadcastMessage(this, $"PLAYER: { msg }");
        }
    }
}
