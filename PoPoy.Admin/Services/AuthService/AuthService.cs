using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.RefreshToken;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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
        public List<User> Users { get; set; } = new List<User>();
        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }
        public async Task<ServiceResponse<AuthResponseDto>> Login(LoginRequest loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/login", loginRequest);
            var content = await result.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<ServiceResponse<AuthResponseDto>>(content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            if (!result.IsSuccessStatusCode)
            {
                return loginResponse;
            }
            await _localStorage.SetItemAsync("authToken", loginResponse.Data.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResponse.Data.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Data.Token);
            return loginResponse;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _navigationManager.NavigateTo("/login");
        }

        public async Task<List<User>> GetUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<User>>("/api/user/getUser");
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

        public async Task<bool> UpdateUser(UserVM user)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/user/userUpdate/{user.Id}", user);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
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
    }
}
