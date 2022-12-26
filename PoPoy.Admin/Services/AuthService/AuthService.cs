using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PoPoy.Admin.Extensions;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.RefreshToken;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;
        private readonly JsonSerializerOptions _options;
        public List<User> Users { get; set; } = new List<User>();
        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<LoginResponse<AuthResponseDto>> Login(LoginRequest loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/login", loginRequest);
            var content = await result.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse<AuthResponseDto>>(content, _options);
            if (!result.IsSuccessStatusCode)
            {
                return loginResponse;
            }
            await _localStorage.SetItemAsync("refreshToken", loginResponse.Data.RefreshToken);
            await _localStorage.SetItemAsync("authToken", loginResponse.Data.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResponse.Data.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Data.Token);
            return loginResponse;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _navigationManager.NavigateTo("/login");
        }

        public async Task<List<UserVM>> GetUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserVM>>("/api/user/getUser");
            return result;
        }

        public async Task<List<User>> SearchUser(string searchText)
        {
            var result = await _httpClient
                .GetFromJsonAsync<List<User>>($"api/user/searchUser/{searchText}");
            return result;
        }

        public async Task<Guid> GetUserId(LoginRequest request)
        {
            var result = await _httpClient.GetFromJsonAsync<Guid>("api/user/getUserId");
            return result;
        }

        public async Task<Guid> GetUserId()
        {
            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = cl.User.GetUserId();
            return Guid.Parse(id);
        }
        public async Task AssignRole(RoleAssignRequest request)
        {
            await _httpClient.PutAsJsonAsync($"api/user/roles/{request.Id}", request);
            await GetRoleAssignRequest(request.Id);
        }

        public async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObj = await _httpClient.GetFromJsonAsync<ServiceResponse<UserVM>>($"/api/user/{id}");
            var roleObj = await _httpClient.GetFromJsonAsync<List<RoleVM>>("api/user/getRoles");
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObj.Data.Roles.Contains(role.Name)
                });
            }
            return roleAssignRequest;
        }
        public async Task<ServiceResponse<User>> GetUserFromId(string id)
        {
            var result = await _httpClient.PostAsync($"/api/user/get-user-from-id?id={id}", null);
            //await result.CheckAuthorized(this);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<User>>();
        }

        public async Task<bool> UpdateUser(UserVM user)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/user/userUpdate/{user.Id}", user);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<ServiceResponse<bool>> CreateUser(CreateUser user)
        {
            var result = await _httpClient.PostAsync($"/api/user/CreateUser", user.ToJsonBody());
            //await result.CheckAuthorized(this);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }


        public async Task<bool> DeleteUser(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/user/{id}");
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<UserVM> GetUserById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<UserVM>($"api/user/getUserById/{id}");
            return result;
        }

        public async Task<List<Address>> GetAddresses()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Address>>("/api/user/getListAddress");
            return result;
        }
        public async Task<List<string>> GetRoleNames()
        {
            var result = await _httpClient.GetFromJsonAsync<List<RoleVM>>("/api/user/getRoles");
            return result.Select(p => p.Name).ToList();
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
            Console.WriteLine("REFRESH TOEKN !!! " + result.IsAuthSuccessful);
            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return result.Token;
        }
        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
    }
}
