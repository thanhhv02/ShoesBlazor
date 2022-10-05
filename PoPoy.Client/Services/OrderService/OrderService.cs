using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using PoPoy.Shared.Paging;
using Microsoft.AspNetCore.WebUtilities;
using System;
using Newtonsoft.Json;
using PoPoy.Shared.Dto;
using System.Linq;
using Syncfusion.Blazor.Kanban.Internal;
using System.Transactions;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace PoPoy.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;

        public OrderService(HttpClient http,
            AuthenticationStateProvider authStateProvider,
            NavigationManager navigationManager, ILocalStorageService localStorage)
        {
            _http = http;
            _navigationManager = navigationManager;
            this._localStorage = localStorage;
        }

        public PagingResponse<OrderOverviewResponse> ListOrderResponse { get; set; } = new PagingResponse<OrderOverviewResponse>();
        public OrderDetailsResponse ListOrderDetailsResponse { get; set; } = new OrderDetailsResponse();

        public event Action OrderDetailsChanged;

        public async Task GetOrderDetails(string? orderId)
        {
            if (orderId != null)
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"/api/Order/get-order-detail-user/{orderId}");
                if (!result.Success)
                {
                    throw new ApplicationException();
                }
                ListOrderDetailsResponse = result.Data;
            }
            OrderDetailsChanged.Invoke();
        }

        public async Task GetOrders(ProductParameters productParameters, string userId)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString()
            };
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");
            var authheader = new AuthenticationHeaderValue("Bearer", savedToken.Replace("\"",""));
            _http.DefaultRequestHeaders.Authorization = authheader;
            var response = await _http.GetAsync(QueryHelpers.AddQueryString($"/api/Order/get-all-order-user/{userId}", queryStringParam));

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            ListOrderResponse.Items = JsonConvert.DeserializeObject<List<OrderOverviewResponse>>(content);
            Console.WriteLine("Count: " + ListOrderResponse.Items.Count);
            ListOrderResponse.MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First());
        }

    }
}
