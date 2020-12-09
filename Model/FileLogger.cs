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

            if (File.Exists("Log.txt"))
            {
                exists = true;
            }
        }

        public override void receiveMessage(string msg)
        {
            if (exists)
            {
                using (StreamWriter w = new StreamWriter("Log.txt"))
                {
                    w.WriteLine(msg);
                }
            }
            else
            {
                Console.WriteLine("Shit broke.");
                //mediator.broadcastMessage(this, "File not found.");
            }
                //sendMessage("Log file not found.");

        }

        public override void sendMessage(string msg)
        {
            mediator.broadcastMessage(this, msg);
        }
    }
}
