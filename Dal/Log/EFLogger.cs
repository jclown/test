using Modobay;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Lib;

namespace Dal.Log
{
    public class EFLogger : ILogger
    {
        private readonly string categoryName;

        public EFLogger(string categoryName) 
        {
            this.categoryName = categoryName;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (categoryName == "Microsoft.EntityFrameworkCore.Database.Command" && eventId.Name != "Microsoft.EntityFrameworkCore.Database.Command.DataReaderDisposing"
                    && (logLevel == LogLevel.Information))
            {
                var logContent = formatter(state, exception);
                var log = $"RequestId:{AppManager.CurrentAppContext?.ReuqestID} {logContent.NewLine()} {logContent}";
                Lib.Log.WriteOperationLog(log);
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
