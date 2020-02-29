using Modobay;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Log
{
    public class EFLoggerProvider : ILoggerProvider
    {
        public EFLoggerProvider()
        {
        }

        public ILogger CreateLogger(string categoryName) => new EFLogger(categoryName);//app, 
        public void Dispose() { }
    }
}
