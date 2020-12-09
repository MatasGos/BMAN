using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LogMediator : ILogMediator
    {
        private List<Logger> senders = new List<Logger>();

        private List<Logger> receivers = new List<Logger>();

        public void addLogReceiver(Logger receiver)
        {
            receivers.Add(receiver);
        }

        public void addLogSender(Logger sender)
        {
            senders.Add(sender);
        }

        public void broadcastMessage(Logger sender, string msg)
        {
            if(sender.type == "Sender")
            {
                foreach (var r in receivers)
                {
                    r.receiveMessage(msg);
                }
            }

            if(sender.type == "Receiver")
            {
                foreach (var s in senders)
                {
                    s.receiveMessage(msg);
                }
            }
        }
    }
}
