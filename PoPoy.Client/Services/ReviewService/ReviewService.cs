using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PoPoy.Client.Extensions;
using PoPoy.Shared.Dto;

namespace PoPoy.Client.Services
{
    public interface IReviewService
    {
        ValueTask<int> PostAsync(Review review);
        ValueTask PutAsync(Review review);
        ValueTask DeleteAsync(int id);
    }

    public class ReviewService : IReviewService
    {
        private readonly HttpClient httpClient;

        public ReviewService(HttpClient httpClient)
            => this.httpClient = httpClient;

        public async ValueTask<int> PostAsync(Review review)
        {
            var result = await httpClient.PostAsJsonAsync("api/review", review);
            string id = await result.Content.ReadAsStringAsync();
            return int.Parse(id);
        }

        public async ValueTask PutAsync(Review review)
        {
            var response = await httpClient.PutAsJsonAsync($"api/review", review);
            await response.HandleError();
        }

        public async ValueTask DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"api/review/{id}");
            await response.HandleError();
        }
    }
}



