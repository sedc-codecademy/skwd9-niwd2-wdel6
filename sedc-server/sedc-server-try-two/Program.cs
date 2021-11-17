using Sedc.Server.Core;
using Sedc.Server.TryTwo;
using Sedc.Server.TryTwo.MsSql;

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

    server
        .ServeStaticFiles()
        .WithProcessor<SqlServerProcessor>("mssql")
        .ServeApi();
        // .WithController(new EchoApiController("not-a-problem"));
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
