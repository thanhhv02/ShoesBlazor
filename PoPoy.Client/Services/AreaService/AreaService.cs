using Microsoft.Extensions.Configuration;
using PoPoy.Shared.Entities.Area;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.AreaService
{
    public class AreaService : IAreaService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public AreaService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.httpClient = new HttpClient() { BaseAddress = new System.Uri(this.configuration["Area"]) };
        }

        public async Task<Area> GetComune(string code)
        {
            return await httpClient.GetFromJsonAsync<Area>($"commune?district={code}");
        }

        public async Task<Area> GetDistrict(string code)
        {
            return await httpClient.GetFromJsonAsync<Area>($"district?province={code}");
        }

        public async Task<Area> GetProvince()
        {
            return await httpClient.GetFromJsonAsync<Area>("province");
        }
    }
}
