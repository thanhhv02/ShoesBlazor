using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using PoPoy.Admin.Services.AuthService;
using PoPoy.Client.Extensions;
using PoPoy.Shared.Dto;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.BroadCastService
{
    public class BroadCastService : IBroadCastService
    {
        private readonly HttpClient httpClient;
        private readonly IAuthService authService;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IConfiguration configuration;

        public BroadCastService(HttpClient httpClient, IAuthService authService, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.authService = authService;
            this.localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
            this.configuration = configuration;
        }

        public async Task<ServiceResponse<List<NotificationDto>>> GetNotificationsByUserJwt()
        {

            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = cl.User.GetUserId();
            var result = await httpClient.GetFromJsonAsync<ServiceResponse<List<NotificationDto>>>($"/api/BroadCast/GetAllNotiByUserId?id={id}");


            return result;
        }

        public async Task SendNotiAllAdmin(NotiSendConfig config)
        {
            var data = SetConfig(config);
            var resp = await httpClient.PostAsync($"/api/BroadCast/SendNotiAllAdmin", data.ToJsonBody());
        }

        public async Task SendNotiUserId(NotiSendConfig config, Guid UserId)
        {
            var data = SetConfig(config);
            if (UserId != Guid.Empty)
            {
                data.UserId = UserId;
                var resp = await httpClient.PostAsync($"/api/BroadCast/SendNotiUserId", data.ToJsonBody());
            }
        }

        private CreateOrUpdateNotiDto SetConfig(NotiSendConfig config)
        {
            CreateOrUpdateNotiDto data = new CreateOrUpdateNotiDto()
            {
                Data = config.Data,
                DataUrl = config.DataUrl,
                Title = config.Title,
                Message = config.Message,
            };
            return data;

        }


        public async Task ReadNoti(Guid notiId)
        {

            var resp = await httpClient.PostAsync($"/api/BroadCast/ReadNoti?notiId={notiId}", null);
        }

        public async Task ReadAllNoti()
        {
            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = cl.User.GetUserId();
            var resp = await httpClient.PostAsync($"/api/BroadCast/ReadAllNoti?userId={id}", null);
        }

        public async Task<HubConnection> BuidHubWithToken()
        {
            string token = await localStorageService.GetItemAsStringAsync("authToken");
            token = token.Remove(0, 1);
            token = token.Remove(token.Length - 1, 1);
            var hostApi = configuration["BackendApiUrl"];
            return new HubConnectionBuilder().WithUrl(hostApi + "/notificationHub", options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(token);
            }).WithAutomaticReconnect().Build();
        }

        public async Task StartAsync(HubConnection hubConnection)
        {
            await hubConnection.StartAsync().ContinueWith(t => {
                if (t.IsFaulted)
                    Console.WriteLine("hub error: " + t.Exception.GetBaseException());
                else
                    Console.WriteLine("Connected to Hub ^^");
            });
        }
    }
}
