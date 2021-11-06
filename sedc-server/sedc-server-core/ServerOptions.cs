using System;
using System.Collections.Generic;

namespace Sedc.Server.Core
{

    public class ServerOptions
    {
        public int Port { get; set; }

        public List<string> AllowedMethods { get; set; }

        public IRequestProcessor Processor { get; set; }

        public ILogger Logger { get; set; }

        internal static ServerOptions MergeWithDefault(ServerOptions options)
        {
            if (options == null)
            {
                return ServerOptions.Default;
            }

            if (options.Port == default(int))
            {
                options.Port = ServerOptions.Default.Port;
            }

            if (options.AllowedMethods == default(List<string>))
            {
                options.AllowedMethods = ServerOptions.Default.AllowedMethods;
            }

            if (options.Processor == default(IRequestProcessor))
            {
                options.Processor = ServerOptions.Default.Processor;
            }

            if (options.Logger == default(ILogger))
            {
                options.Logger = ServerOptions.Default.Logger;
            }

            return options;
        }

        internal static readonly ServerOptions Default = new ServerOptions {
            Port = 664, // The Neighbour of the Beast
            AllowedMethods = new List<string> { "GET", "POST" },
            Processor = new DefaultRequestProcessor(),
            Logger = new ConsoleLogger()
        };

    }
}