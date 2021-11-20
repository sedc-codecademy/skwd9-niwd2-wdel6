using Sedc.Server.Core;
using Sedc.Server.Core.Logging;
using Sedc.Microservice.SqlServerProcessor;

var logger = new CompositeLogger();
logger.AddLogger(new NiceConsoleLogger { Level = LogLevel.Debug });
logger.AddLogger(new FileLogger("log.txt"));

try
{
    Server server = new Server(new ServerOptions
    {
        Port = 8081,
        Logger = logger
    });

    server
        .WithProcessor<SqlServerRequestProcessor>("mssql")
        .Start();
}
catch (ApplicationException aex)
{
    var oldColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("Application Exception: ");
    Console.WriteLine(aex.Message);
    Console.ForegroundColor = oldColor;
}