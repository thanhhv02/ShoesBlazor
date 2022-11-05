using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using Syncfusion.Blazor.Kanban.Internal;
using Syncfusion.Blazor.PdfViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PoPoy.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<Product> SlideProducts { get; set; }
        PagingResponse<Product> Products { get; set; }
        string Message { get; set; }
        Task GetAll(ProductParameters productParameters, string categoryUrl = null);
        Task GetProductForSlide();
        Task<ServiceResponse<Product>> Get(int id);
        Task<List<ProductSize>> GetSizeProduct(int id);
        ValueTask<List<ProductQuantity>> FilterAllByIdsAsync(int[] ids, int[] sizes);
        Task SearchProducts(ProductParameters productParameters, string searchText);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
    }
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IPublicReviewService publicReviewService;

        public ProductService(HttpClient httpClient, IConfiguration configuration, IPublicReviewService PublicReviewService)
        {
            _httpClient = httpClient;
            this._configuration = configuration;
            publicReviewService = PublicReviewService;
        }

        public PagingResponse<Product> Products { get; set; } = new PagingResponse<Product>();

        public List<Product> SlideProducts { get; set; }
        public string Message { get; set; } = "Loading products...";

        public event Action ProductsChanged;

        public async ValueTask<List<ProductQuantity>> FilterAllByIdsAsync(int[] ids, int[] sizes)
        {
            var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
            var query2 = System.Web.HttpUtility.ParseQueryString(string.Empty);
            foreach (var id in ids)
            {
                query.Add("ids", id.ToString());
            }
            foreach (var size in sizes)
            {
                query.Add("sizes", size.ToString());
            }
            var response = await _httpClient.GetAsync($"api/product/filter?{query}&{query2}");
            //await response.HandleError();

            return await response.Content.ReadFromJsonAsync<List<ProductQuantity>>();
        }
        public async Task<ServiceResponse<Product>> Get(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"/api/product/" + id);
            return result;
        }

        public async Task GetAll(ProductParameters productParameters, string? categoryUrl)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString()
            };
            var response = categoryUrl == null ?
                        await _httpClient.GetAsync(QueryHelpers.AddQueryString($"/api/product", queryStringParam)) :
                        await _httpClient.GetAsync(QueryHelpers.AddQueryString($"/api/product/category/{categoryUrl}", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Products.Items = JsonConvert.DeserializeObject<List<Product>>(content);

            Products.MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First());
            ProductsChanged.Invoke();
        }

        public async Task GetProductForSlide()
        {
            ProductParameters productParameters = new ProductParameters();
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString()
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"/api/product", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var result = JsonConvert.DeserializeObject<List<Product>>(content);
            SlideProducts = result.OrderByDescending(x => x.Views).Take(4).ToList();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _httpClient
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");
            return result.Data;
        }

        public async Task<List<ProductSize>> GetSizeProduct(int id)
        {
            var getSizeProduct = await _httpClient.GetFromJsonAsync<List<ProductSize>>($"/api/product/get-size-product/" + id);
            return getSizeProduct;
        }

        public async Task SearchProducts(ProductParameters productParameters, string searchText)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString()
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"api/product/search/{searchText}", queryStringParam));

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            if (content != null)
            {
                Products.Items = JsonConvert.DeserializeObject<List<Product>>(content);
                Products.MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First());
            }
            if (Products.Items.Count == 0) Message = "No products found.";
            ProductsChanged?.Invoke();
        }
    }
}
