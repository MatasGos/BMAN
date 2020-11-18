using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Server
{
    public class HubTest : IHubCallerClients
    {
        public List<ClientTest> clientList = new List<ClientTest>();

        public void AddClient(Player player)
        {
            clientList.Add(new ClientTest(player));
        }
        public IClientProxy Caller => throw new NotImplementedException();

        public IClientProxy Others => throw new NotImplementedException();

        public IClientProxy All => throw new NotImplementedException();

        public IClientProxy AllExcept(IReadOnlyList<string> excludedConnectionIds)
        {
            throw new NotImplementedException();
        }

        public IClientProxy Client(string connectionId)
        {
            foreach(var client in clientList)
            {
                if (client.client.id.Equals(connectionId))
                {
                    return client;
                }
            }
            return null;
        }

        public IClientProxy Clients(IReadOnlyList<string> connectionIds)
        {
            IClientProxy toReturn = null;
            foreach(var x in clientList)
            {
                if (x.client.id.Equals(connectionIds[0]))
                {
                    toReturn = x;
                }
            }
            return toReturn;
        }

        public IClientProxy Group(string groupName)
        {
            throw new NotImplementedException();
        }

        public IClientProxy GroupExcept(string groupName, IReadOnlyList<string> excludedConnectionIds)
        {
            throw new NotImplementedException();
        }

        public IClientProxy Groups(IReadOnlyList<string> groupNames)
        {
            throw new NotImplementedException();
        }

        public IClientProxy OthersInGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        public IClientProxy User(string userId)
        {
            throw new NotImplementedException();
        }

        public IClientProxy Users(IReadOnlyList<string> userIds)
        {
            throw new NotImplementedException();
        }
    }
}
