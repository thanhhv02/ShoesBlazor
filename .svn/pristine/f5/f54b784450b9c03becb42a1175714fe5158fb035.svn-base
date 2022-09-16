using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CateVM>> GetAllCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CateVM>>("/api/category");
            return result;
        }
        public async Task<CateVM> GetCategoryById(int categoryId)
        {
            var result = await _httpClient.GetFromJsonAsync<CateVM>($"api/category/{categoryId}");
            return result;
        }
        public async Task<List<CateVM>> SearchCategory(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<List<CateVM>>($"api/category/searchCategory/{searchText}");
            return result;
        }
        public async Task<bool> CreateCategory(CategoryCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/category", request);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task UpdateCategory(CateVM category)
        {
            await _httpClient.PutAsJsonAsync($"api/product/{category.Id}", category);
        }

        public async Task DeleteCategory(int categoryId)
        {
            await _httpClient.DeleteAsync($"/api/product/{categoryId}");
        }


    }
}
