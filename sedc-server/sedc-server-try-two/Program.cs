using Sedc.Server.Core;

using System;
using System.Linq;
using System.Reflection;

namespace sedc_server_try_two
{


    class Program
    {
        static void Main(string[] args)
        {
            var logger = new CompositeLogger();
            logger.AddLogger(new NiceConsoleLogger { Level = LogLevel.Debug });
            logger.AddLogger(new FileLogger("log.txt"));

            try
            {
                Server server = new Server(new ServerOptions
                {
                    Port = 668,
                    Logger = logger
                });

                server.ServeStaticFiles();
                server.ServeApi().WithController(new EchoApiController("not-a-problem"));
                server.Start();
            }
            catch (ApplicationException aex)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Application Exception: ");
                Console.WriteLine(aex.Message);
                Console.ForegroundColor = oldColor;
            }
        }
    }
}
