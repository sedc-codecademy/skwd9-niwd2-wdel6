using Sedc.Server.Core;
using Sedc.Server.Core.Entities;
using Sedc.Server.Core.Helpers.ServerHelpers;
using System;

namespace sedc_server_try_two
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(new ServerOptions
            {
                Port = 668
            });
            server.Start();
        }
    }
}
