using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    interface IPlayerObserver
    {
        public abstract void update(IHubCallerClients context, string jsonMap, string jsonPlayers);
    }
}
