using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace XeemAPI.WebSocket
{
    public class RequestHub: Hub
    {
        public override Task OnConnected()
        {
            Console.WriteLine("connected");
            return base.OnConnected();
        }
        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }

        public void BroadcastMessages(string message)
        {
         
        }
    }
}