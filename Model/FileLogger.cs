using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model
{
    public class FileLogger : Logger
    {
        private bool exists = false;
        public FileLogger(ILogMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
            type = "Receiver";

            string file = "Log.txt";

            if (File.Exists(file))
            {
                exists = true;
            }
        }

        public override void receiveMessage(string msg)
        {
            if (exists)
            {
                using (StreamWriter w = new StreamWriter(@"..\..\..\Log.txt", true))
                {
                    w.WriteLine(msg);
                }
            }
            else
            {
                sendMessage("Log file not found.");
            }

        }

        public override void sendMessage(string msg)
        {
            mediator.broadcastMessage(this, msg);
        }
    }
}
