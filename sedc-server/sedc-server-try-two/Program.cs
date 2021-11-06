using Sedc.Server.Core;

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
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Server server = new Server(new ServerOptions
                {
                    Port = 668,
                    Processor = new FileRequestProcessor(@"public-sedc"),
                    Logger = new NiceConsoleLogger { Level = LogLevel.Information }
                });
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
