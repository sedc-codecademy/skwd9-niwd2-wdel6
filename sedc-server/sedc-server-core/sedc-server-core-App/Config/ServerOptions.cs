using System.Collections.Generic;

namespace Sedc.Server.Core.sedc_server_core_App.Config
{
    public class ServerOptions
    {
        public int Port { get; set; }

        public List<string> AllowedMethods { get; set; }

        internal static readonly ServerOptions Default = new ServerOptions {
            Port = 664, // The Neighbour of the Beast
            AllowedMethods = new List<string> { "GET", "POST"}
        };

    }
}