using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger ()
        {
            Level = LogLevel.Information;
        }

        public LogLevel Level { get ; set ; }

        public void Log(string message, LogLevel level = LogLevel.Information)
        {
            if (level >= Level) {
                Console.WriteLine($"[{level.ToString().ToUpperInvariant()}] {message}");
            }
        }
    }
}
