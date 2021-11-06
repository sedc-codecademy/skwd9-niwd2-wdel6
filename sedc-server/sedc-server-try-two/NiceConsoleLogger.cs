using Sedc.Server.Core;

using System;

namespace sedc_server_try_two
{
    class NiceConsoleLogger : ILogger
    {
        public LogLevel Level { get; set; }

        public void Log(string message, LogLevel level = LogLevel.Information)
        {
            if (level < Level) {
                return;
            }

            if (level == LogLevel.Error) {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            if (level == LogLevel.Warning) {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            if (level == LogLevel.Information) {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (level == LogLevel.Debug) {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.WriteLine(message);

            Console.ResetColor();
        }
    }
}