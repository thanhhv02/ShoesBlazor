using PoPoy.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.LogService
{
    public interface ILogService
    {
        Task<List<Logs>> GetAll();
        Task<Logs> GetLogById(int id);
        Task<bool> Clear();
    }
}
