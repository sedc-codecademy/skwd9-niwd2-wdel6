﻿using Sedc.Server.Core;
using Sedc.Server.Core.Logging;

var logger = new CompositeLogger();
logger.AddLogger(new NiceConsoleLogger { Level = LogLevel.Debug });
logger.AddLogger(new FileLogger("log.txt"));

try
{
    Server server = new Server(new ServerOptions
    {
        Port = 8084,
        Logger = logger
    });

    server
        .ServeStaticFiles("wwwroot")
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