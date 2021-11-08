using Sedc.Server.Core;

using System;
using System.IO;

namespace sedc_server_try_two
{
    class FileLogger : ILogger
    {
        public string LogFile { get; private set; } 

        public FileLogger(string logFile, LogLevel level = LogLevel.Information) {
            LogFile = logFile;
            Level = level;
        }

        public LogLevel Level { get; set; }

        public void Log(string message, LogLevel level = LogLevel.Information)
        {
            if (level < Level) {
                return;
            }
            string timeStamp = $"{DateTime.UtcNow.ToShortDateString()} {DateTime.UtcNow.ToLongTimeString()}";
            string line = $"{timeStamp} [{level.ToString().ToUpperInvariant()}] {message}{Environment.NewLine}";

            // todo: this method can throw all kinds of exceptions
            File.AppendAllText(LogFile, line);
        }
    }
}