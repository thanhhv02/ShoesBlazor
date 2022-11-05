using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PoPoy.Client.Extensions;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }
        public async Task<ServiceResponse<string>> Login(LoginRequest loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/login", loginRequest);
            var content = await result.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<ServiceResponse<string>>(content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            if (!result.IsSuccessStatusCode)
            {
                return loginResponse;
            }
            await _localStorage.SetItemAsync("authToken", loginResponse.Data);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResponse.Data);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Data);
            return loginResponse;
        }

        public async Task<ServiceResponse<bool>> Register(RegisterRequest registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/register", registerRequest);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task Logout()
        {
            //await _localStorage.RemoveItemAsync("authToken");
            //await _localStorage.RemoveItemAsync("getUserId");
            await _localStorage.ClearAsync();
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _navigationManager.NavigateTo("/login", true);
        }

        public async Task<ServiceResponse<string>> ForgotPassword(string email)
        {
            var result = await _httpClient.PostAsync($"/api/user/forgot-password?email={email}", null);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<User>> GetUserFromId(string id)
        {
            var result = await _httpClient.PostAsync($"/api/user/get-user-from-id?id={id}", null);
            result.CheckAuthorized(this);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<User>>();
        }

        public async Task<ServiceResponse<string>> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/change-password", changePasswordRequest);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<string>> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/reset-password", resetPasswordRequest);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
        public async Task<Guid> GetUserId(LoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/getUserId", request);
            return await result.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<string> Checkout(List<Cart> cartItems)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/checkout", cartItems);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<bool> CreateAddress(Address address)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/user/address", address);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<Address> GetAddress(Guid userId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Address>>($"api/user/getAddress/?userId=" + userId);
            return response.Data;
        }

        public async Task<Address> AddOrUpdateAddress(Address address, Guid userId)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/user/addOrUpdateAddress/?userId=" + userId, address);
            return response.Content.ReadFromJsonAsync<ServiceResponse<Address>>().Result.Data;
        }

        public async Task<string> MakePayPalPayment(double total)
        {
            var url = await _httpClient.GetStringAsync("api/user/checkoutPayPal/?total=" + total);
            return url;
        }

        public async Task<string> MakeVNPayPayment(double total)
        {
            var url = await _httpClient.GetStringAsync("api/user/checkoutVNPay/?total=" + total);
            return url;
        }
    }
}
