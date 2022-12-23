using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using PoPoy.Admin.Services.BroadCastService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Admin.Shared.Notify
{
    public partial class Notify
    {
        [Inject] private HubConnection hubConnection { get; set; }
        [Inject] private IConfiguration configuration { get; set; }
        [Inject] public ILocalStorageService localStorageService { get; set; }
        [Inject] public IBroadCastService broadCastService { get; set; }

        [Inject] public IToastService toastService { get; set; }
        [Inject] public IJSRuntime jSRuntime { get; set; }

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
  
            SubscribeBroadCastNoti(broadCastType: BroadCastType.Notify,
                async noti =>
                {
                    notifications.Add(noti);
                    notifications = notifications.OrderByDescending(p => p.Created).ToList();
                    toastService.ShowInfo(noti.Message , noti.Title);
                    await jSRuntime.InvokeVoidAsync("PlayTing");
                    StateHasChanged();
                });

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
