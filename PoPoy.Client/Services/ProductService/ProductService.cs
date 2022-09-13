﻿using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.ProductService
{
    public interface IProductService
    {
        Task<PagingResponse<Product>> GetAll(ProductParameters productParameters);
        Task<ServiceResponse<Product>> Get(int id);
        ValueTask<List<Product>> FilterAllByIdsAsync(int[] ids);
    }
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            this._configuration = configuration;
        }

        public async ValueTask<List<Product>> FilterAllByIdsAsync(int[] ids)
        {
            var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
            foreach (var id in ids)
            {
                query.Add("ids", id.ToString());
            }

            var response = await _httpClient.GetAsync($"api/product/filter/ids?{query}");
            //await response.HandleError();

            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }
        public async Task<ServiceResponse<Product>> Get(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"/api/product/"+id);
            return result;
        }

        public async Task<PagingResponse<Product>> GetAll(ProductParameters productParameters)
        {
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
            var pagingResponse = new PagingResponse<Product>
            {
                Items = JsonConvert.DeserializeObject<List<Product>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            
            return pagingResponse;
            
        }
    }
}
