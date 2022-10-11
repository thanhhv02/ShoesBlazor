using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PoPoy.Client.Extensions;
using PoPoy.Shared.Dto;

namespace PoPoy.Client.Services
{
    public interface IPublicReviewService
    {
        ValueTask<Review> GetAsync(int id);
        ValueTask<List<Review>> FilterByProductIdAsync(int productId);
    }

    public class PublicReviewService : IPublicReviewService
    {
        private readonly HttpClient httpClient;

        public PublicReviewService(HttpClient publicHttpClient)
            => this.httpClient = publicHttpClient;

        public async ValueTask<List<Review>> FilterByProductIdAsync(int productId)
        {
            var response = await httpClient.GetAsync($"api/review/filter/{productId}");
            await response.HandleError();

            return await response.Content.ReadFromJsonAsync<List<Review>>();
        }

        public async ValueTask<Review> GetAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/review/{id}");
            await response.HandleError();

            return await response.Content.ReadFromJsonAsync<Review>();
        }

    }
}



