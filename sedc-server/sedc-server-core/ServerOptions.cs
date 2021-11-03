using System.Collections.Generic;

namespace Sedc.Server.Core
{
    public class ServerOptions
    {
        public int Port { get; set; }

        public List<string> AllowedMethods { get; set; }

        public IRequestProcessor Processor { get; set; }

        internal static readonly ServerOptions Default = new ServerOptions {
            Port = 664, // The Neighbour of the Beast
            AllowedMethods = new List<string> { "GET", "POST" },
            Processor = new DefaultRequestProcessor()
        };

    }
}