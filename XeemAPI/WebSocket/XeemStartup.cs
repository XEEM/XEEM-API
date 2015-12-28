using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.IO;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(XeemAPI.WebSocket.XeemStartup))]

namespace XeemAPI.WebSocket
{
    public class XeemStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfig = new HubConfiguration();
            hubConfig.EnableDetailedErrors = true;
            //hubConfig.EnableJavaScriptProxies = false;
            hubConfig.EnableJSONP = true;

            app.MapSignalR(hubConfig);
        }

        string getTime()
        {
            return DateTime.Now.Millisecond.ToString();
        }
    }
}
