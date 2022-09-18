using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<CateVM> Categories { get; set; } = new List<CateVM>();

        public event Action OnChange;

        public async Task GetCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CateVM>>("api/Category");
            if (response != null && response != null)
                this.Categories = response;
        }

    }
}
