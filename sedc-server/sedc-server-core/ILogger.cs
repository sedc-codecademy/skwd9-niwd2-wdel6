using System;

namespace Sedc.Server.Core
{
    public enum LogLevel {
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Critical = 5
    }

    public interface ILogger
    {
        public LogLevel Level { get; set; }
        void Log(string message, LogLevel level = LogLevel.Information);

        void LogException(Exception ex, LogLevel level = LogLevel.Error) {
            Log(ex.Message, level);
        }

        void Debug(string message) => Log(message, LogLevel.Debug);
        void Info(string message) => Log(message, LogLevel.Information);
        void Warning(string message) => Log(message, LogLevel.Warning);
        void Error(string message) => Log(message, LogLevel.Error);
        void Critical(string message) => Log(message, LogLevel.Critical);


    }
}