using PoPoy.Shared.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.LogService
{
    public class LogService : ILogService
    {
        private readonly HttpClient _httpClient;
        public LogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Logs>> GetLogs()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Logs>>("/api/log/GetLogs");
            return result;
        }

        public async Task<Logs> GetLogById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Logs>($"/api/log/GetLogById?id={id}");
            return result;
        }

        public async Task ClearLog()
        {
            var result = await _httpClient.DeleteAsync($"/api/Log/ClearLog");
        }
    }
}
