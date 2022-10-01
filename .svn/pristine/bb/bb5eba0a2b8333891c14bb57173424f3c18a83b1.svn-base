using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.UserAvatarService
{
    public class UserAvatarService : IUserAvatarService
    {
        private readonly HttpClient _httpClient;

        public UserAvatarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public event Action OnAvatarChanged;

        public async Task<List<UploadResult>> UploadAvatar(MultipartFormDataContent multipartFormDataContent, string id)
        {
            var response = await _httpClient.PostAsync($"/api/user/upload-image?id={id}", multipartFormDataContent);
            OnAvatarChanged.Invoke();
            return await response.Content.ReadFromJsonAsync<List<UploadResult>>();
        }
    }
}
