using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using PoPoy.Admin.Extensions;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Entities.OrderDto;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
        public OrderService(HttpClient httpClient,
                           ILocalStorageService localStorage,
                           NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Order>>("/api/order");
            return result;
        }

        public async Task<List<OrderDetails>> GetOrderDetails(string orderId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<OrderDetails>>($"/api/order/orderDetails/{orderId}");
            return result;
        }

        public async Task<Order> GetOrderWithUser(string orderId)
        {
            var result = await _httpClient.GetFromJsonAsync<Order>($"/api/order/GetOrderWithUser/{orderId}");
            return result;
        }
        public async Task<bool> AssignShipper(AssignShipperDto model)
        {
            var resp = await _httpClient.PostAsync($"/api/order/AssignShipper", model.ToJsonBody());
            return resp.IsSuccessStatusCode;
        }
        public async Task<List<SelectItem>> GetShippers()
        {
            var resp = await _httpClient.GetFromJsonAsync<List<SelectItem>>($"/api/user/GetShippers");
            return resp;
        }

        public async Task<List<Order>> GetOrderByShipper(OrderShipperSearchDto input)
        {
            var result = await _httpClient.PostAsync($"/api/order/GetOrderByShipper", input.ToJsonBody());
            //await result.CheckAuthorized(this);
            return await result.Content.ReadFromJsonAsync<List<Order>>();
        }

        public async Task<bool> UpdateStatusOrder(UpdateStatusOrderDto input)
        {
            var result = await _httpClient.PostAsync($"/api/order/UpdateStatusOrder", input.ToJsonBody());
            //await result.CheckAuthorized(this);
            return await result.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<List<Order>> SearchOrder(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Order>>($"api/order/searchOrder/{searchText}");
            return result;
        }
        public async Task DeleteOrder(string orderId)
        {
            await _httpClient.DeleteAsync($"/api/order/{orderId}");
        }
    }
}
