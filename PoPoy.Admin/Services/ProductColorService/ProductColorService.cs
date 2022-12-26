using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.ProductColorService
{
    public class ProductColorService : IProductColorService
    {
        private readonly HttpClient _httpClient;
        public ProductColorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductColorDto>> GetAllProductColor()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductColorDto>>("/api/ProductColor");
            return result;
        }

        public async Task<ProductColorDto> GetProductColorById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ProductColorDto>($"api/ProductColor/{id}");
            return result;
        }
        public async Task<bool> CreateProductColor(ProductColorDto model)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/ProductColor", model);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }


        public async Task<bool> UpdateProductColor(ProductColorDto model)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/ProductColor", model);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task DeleteProductColor(int id)
        {
            await _httpClient.DeleteAsync($"/api/ProductColor/{id}");
        }
    }

}
