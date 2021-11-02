using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Helpers.ServerHelpers
{
    public class ServerOptions
    {
        public int Port { get; set; }

        public List<string> AllowedMethods { get; set; }

        internal static readonly ServerOptions Default = new ServerOptions
        {
            Port = 664, // The Neighbour of the Beast
            AllowedMethods = new List<string> { "GET", "POST" }
        };

    }
}
