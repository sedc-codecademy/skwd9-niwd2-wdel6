using Sedc.Server.Core;
using Sedc.Server.Core.Entities;
using Sedc.Server.Core.Logging.Implementations;
using Sedc.Server.Core.Logging.Interfaces;
using Sedc.Server.Core.Response;
using Sedc.Server.Core.Response.Implementations;
using Sedc.Server.Core.Services.RequestService.Implementations;
using Sedc.Server.Core.Services.RequestService.Interfaces;
using System;

namespace sedc_server_try_two
{
    class MyRequestProcessor : IRequestProcessor
    {
        public IResponse Process(Request request, ILogger logger)
        {
            return new TextResponse
            {
                Message = $@"
<html>
    <head>
        <title>Custom SEDC Server</title>
    </head>
    <body>
        Hi, I'm a <b>Custom SEDC Server</b> and <i>I don't like you</i>! You used the {request.Method} method on the {request.Address} URL
    </body>
</html>",
                Status = Status.OK
            };
        }

        public bool ShouldProcess(Request request)
        {
            return true;
        }
    }

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
                server.RegisterProcessor(new FileRequestProcessor("public-sedc"));
                server.RegisterProcessor(new ApiRequestProcessor());
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
