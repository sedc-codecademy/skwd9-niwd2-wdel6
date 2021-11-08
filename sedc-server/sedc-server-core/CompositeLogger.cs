using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public class CompositeLogger : ILogger
    {
        private List<ILogger> loggers = new List<ILogger>();
        public LogLevel Level { get ; set ; }

        public CompositeLogger ()
        {
            Level = LogLevel.Information;
        }

        public void AddLogger(ILogger logger) {
            loggers.Add(logger);
        }

        public void Log(string message, LogLevel level = LogLevel.Information)
        {
            foreach (var logger in loggers)
            {
                logger.Log(message, level);
            } 
        }
    }
}
