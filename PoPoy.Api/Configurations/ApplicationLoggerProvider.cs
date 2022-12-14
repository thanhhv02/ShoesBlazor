using Microsoft.Extensions.Logging;
using PoPoy.Api.Data;

namespace PoPoy.Api.Configurations
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly DataContext dataContext;

        public ApplicationLoggerProvider(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLoger(dataContext);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
