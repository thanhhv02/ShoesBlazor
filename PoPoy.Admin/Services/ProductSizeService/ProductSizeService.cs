using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.ProductSizeService
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly HttpClient _httpClient;
        public ProductSizeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductSizeDto>> GetAllProductSize()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductSizeDto>>("/api/ProductSize");
            return result;
        }

        public async Task<ProductSizeDto> GetProductSizeById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ProductSizeDto>($"api/ProductSize/{id}");
            return result;
        }
        public async Task<bool> CreateProductSize(ProductSizeDto model)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/ProductSize", model);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }


        public async Task<bool> UpdateProductSize(ProductSizeDto model)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/ProductSize", model);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task DeleteProductSize(int id)
        {
            await _httpClient.DeleteAsync($"/api/ProductSize/{id}");
        }
    }
 
}
