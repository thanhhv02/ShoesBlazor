using Microsoft.Extensions.Logging;
using PoPoy.Api.Data;
using PoPoy.Shared.Entities;
using System;
using System.Security.Cryptography;

namespace PoPoy.Api.Configurations
{
    public class DatabaseLoger : ILogger
    {
        private readonly DataContext dataContext;

        public DatabaseLoger(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Logs log = new();
            log.LogLevel = (Shared.Enum.LogLevelApp)(int)logLevel;
            log.EventName = eventId.ToString();
            log.Message = exception?.Message ?? string.Empty;
            log.StackTrace = exception?.StackTrace ?? string.Empty;
            log.Source = exception?.Source ?? string.Empty;
            log.CreateDate = DateTime.UtcNow;

            dataContext.Add(log);
            dataContext.SaveChanges();
        }
    }
}
