using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using PoPoy.Client.Services.BroadCastService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Client.Shared.Notify
{
    public partial class Notify
    {
         private HubConnection hubConnection { get; set; }
        [Inject] private IConfiguration configuration { get; set; }
        [Inject] public ILocalStorageService localStorageService { get; set; }
        [Inject] public IBroadCastService broadCastService { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }



        private List<NotificationDto> notifications = new();

        async Task Reload()
        {
            var result = await broadCastService.GetNotificationsByUserJwt();
            notifications = result.Data;
            notifications = notifications.OrderByDescending(p => p.Created).ToList();
            StateHasChanged();

        }
        protected override async Task OnInitializedAsync()
        {
            await Reload();
            hubConnection = await broadCastService.BuidHubWithToken();
            SubscribeBroadCastNoti(broadCastType: BroadCastType.Notify,
                noti =>
                {
                    notifications.Add(noti);
                    notifications =  notifications.OrderByDescending(p => p.Created).ToList();
                    StateHasChanged();
                });

            await broadCastService.StartAsync(hubConnection);
        }

    
        private void SubscribeBroadCastNoti(string broadCastType, Action<NotificationDto> action)
        {
            hubConnection.On<NotificationDto>(broadCastType, action);
        }
        private async Task ReadNoti(NotificationDto noti)
        {
            await broadCastService.ReadNoti(noti.Id);
            notifications.ForEach(i =>
            {
                if (i.Id == noti.Id)
                {
                    i.IsRead = true;
                    return;
                }
            });
            navigationManager.NavigateTo(noti.DataUrl);
        }
        private async Task ReadAllNoti()
        {
            await broadCastService.ReadAllNoti();
            await Reload();

        }
    }


}
