using PoPoy.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.LogService
{
    public interface ILogService
    {
        Task<List<Logs>> GetLogs();
        Task<Logs> GetLogById(int id);
        Task ClearLog();
    }
}
