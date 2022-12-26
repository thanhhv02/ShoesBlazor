using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using PoPoy.Client.Extensions;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Client.Services
{
    public interface IPublicReviewService
    {
        ValueTask<Review> GetAsync(int id);
        ValueTask<PagingResponse<Review>> FilterByProductIdAsync(int productId, ProductParameters productParameters);
    }

    public class PublicReviewService : IPublicReviewService
    {
        private readonly HttpClient httpClient;

        public PublicReviewService(HttpClient publicHttpClient)
            => this.httpClient = publicHttpClient;

        public async ValueTask<PagingResponse<Review>> FilterByProductIdAsync(int productId, ProductParameters productParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString()
            };

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString($"api/review/filter/{productId}", queryStringParam));
            await response.HandleError();
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<Review>
            {
                Items = JsonConvert.DeserializeObject<List<Review>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

        public async ValueTask<Review> GetAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/review/{id}");
            await response.HandleError();

            return await response.Content.ReadFromJsonAsync<Review>();
        }

    }
}



