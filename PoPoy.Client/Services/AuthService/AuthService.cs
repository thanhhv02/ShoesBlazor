using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PoPoy.Client.Extensions;
using PoPoy.Client.Services.HttpRepository;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.RefreshToken;
using PoPoy.Shared.ViewModels;
using Syncfusion.Blazor.Kanban.Internal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
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
        private readonly JsonSerializerOptions _options;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage, NavigationManager navigationManager
                          )
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<ServiceResponse<AuthResponseDto>> Login(LoginRequest loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/login", loginRequest);
            var content = await result.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<ServiceResponse<AuthResponseDto>>(content,_options);
            if (!result.IsSuccessStatusCode)
            {
                return loginResponse;
            }
            await _localStorage.SetItemAsync("authToken", loginResponse.Data.Token);
            await _localStorage.SetItemAsync("refreshToken", loginResponse.Data.RefreshToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResponse.Data.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Data.Token);
            return loginResponse;
        }

        public async Task<ServiceResponse<bool>> Register(RegisterRequest registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/register", registerRequest);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
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
            //await result.CheckAuthorized(this);
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

        public async Task PaymentPaypal(string paymentId, string payerId, Guid userId)
        {
            await _httpClient.GetFromJsonAsync($"/api/user/paymentPaypal/?paymentId={paymentId}&payerId={payerId}&userId={userId}", null);
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

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
            var tokenDto = JsonSerializer.Serialize(new RefreshTokenDto { Token = token, RefreshToken = refreshToken });
            var bodyContent = new StringContent(tokenDto, Encoding.UTF8, "application/json");
            var refreshResult = await _httpClient.PostAsync("api/token/refresh", bodyContent);
            var refreshContent = await refreshResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponseDto>(refreshContent, _options);
            if (!result.IsAuthSuccessful)
            {
                await Logout();
                return null;
            }
            Console.WriteLine("REFRESH TOEKN !!! "+ result.IsAuthSuccessful);
            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return result.Token;
        }

        public async Task<ServiceResponse<bool>> UpdateUser(User user)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/user/update-user-profile", user);
            return await result.Content.ReadFromJsonAsync<ServiceErrorResponse<bool>>();
        }

        public async Task<bool> DeleteAvatar()
        {
            var result = await _httpClient.DeleteAsync($"api/user/user-avatar");
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task PaymentVnpay(string vnPayTransactionStatus, Guid userId)
        {
            await _httpClient.GetFromJsonAsync($"/api/user/paymentVnpay/?vnPayTransactionStatus={vnPayTransactionStatus}&userId={userId}", null);
        }
    }
}
