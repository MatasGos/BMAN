using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface ILogMediator
    {
        public void addLogSender(Logger sender);

        public void addLogReceiver(Logger receiver);

        public void broadcastMessage(Logger sender, string msg);
    }
}
