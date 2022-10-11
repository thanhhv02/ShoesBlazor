using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
        public ProductService(HttpClient httpClient,
                           ILocalStorageService localStorage,
                           NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }



        public async Task<List<ProductVM>> GetAllProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductVM>>("/api/product/getAllProducts");
            return result;
        }

        public async Task<ProductVM> GetProductById(int productId)
        {
            var result = await _httpClient.GetFromJsonAsync<ProductVM>($"api/product/getProductById/{productId}");
            return result;
        }

        public async Task<List<ProductVM>> SearchProduct(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductVM>>($"api/product/searchProduct/{searchText}");
            return result;
        }

        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/product", request);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task UpdateProduct(ProductVM product)
        {
            await _httpClient.PutAsJsonAsync($"api/product/{product.Id}", product);
        }

        public async Task DeleteFile(int productId)
        {
            await _httpClient.DeleteAsync($"api/product/delete-image/{productId}");
        }

        public async Task DeleteProduct(int productId)
        {
            await _httpClient.DeleteAsync($"/api/product/{productId}");
        }

        public async Task AssignCate(CategoryAssignRequest request)
        {
            await _httpClient.PutAsJsonAsync($"api/product/categories/{request.Id}", request);
            await GetCateAssignRequest(request.Id);
        }

        public async Task<CategoryAssignRequest> GetCateAssignRequest(int productId)
        {
            var prodObj = await _httpClient.GetFromJsonAsync<ProductVM>($"/api/product/getProductById/{productId}");
            var cateObj = await _httpClient.GetFromJsonAsync<List<CateVM>>("api/category");
            var cateAssignRequest = new CategoryAssignRequest();
            foreach (var cate in cateObj)
            {
                cateAssignRequest.Categories.Add(new SelectItem()
                {
                    Id = cate.Id.ToString(),
                    Name = cate.Name,
                    Selected = prodObj.Categories.Contains(cate.Name)
                });
            }
            return cateAssignRequest;
        }

        public async Task AssignSize(SizeAssignRequest request)
        {
            await _httpClient.PutAsJsonAsync($"api/product/sizes/{request.Id}", request);
            await GetSizeAssignRequest(request.Id);
        }

        public async Task<SizeAssignRequest> GetSizeAssignRequest(int productId)
        {
            var prodObj = await _httpClient.GetFromJsonAsync<ProductVM>($"/api/product/getProductById/{productId}");
            var sizeObj = await _httpClient.GetFromJsonAsync<List<ProductSize>>("api/product/getSizes");
  
            var sizeAssignRequest = new SizeAssignRequest();
            foreach (var size in sizeObj)
            {
                sizeAssignRequest.Sizes.Add(new SelectItem()
                {
                    Id = size.Id.ToString(),
                    Name = size.Size.ToString(),
                    Selected = prodObj.Sizes.Contains(size.Id.ToString()),
                    Qty = await GetQuantity(size.Id,prodObj.Id)
                });
            }
            return sizeAssignRequest;
        }
        public async Task<int> GetQuantity(int sizeid, int prodid)
        {
            var res = await _httpClient.GetAsync($"/api/product/get-quantity-of-product?sizeId={sizeid}&Prodid={prodid}");
            var result = await res.Content.ReadAsStringAsync();
            return Convert.ToInt32(result.ToString());
        }
    }
}
