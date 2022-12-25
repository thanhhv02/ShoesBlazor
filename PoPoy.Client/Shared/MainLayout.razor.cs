using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using PoPoy.Client.Services.BroadCastService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Radzen;
using PoPoy.Client.Helper;

namespace PoPoy.Client.Shared
{
    public partial class MainLayout
    {
        [Inject] public HubConnection hubConnection { get; set; }
        [Inject] private IBroadCastService broadCastService { get; set; }
        [Inject] private IJSRuntime jSRuntime { get; set; }
        [Inject] private IToastService toastService { get; set; }
        [Inject] private DialogService DialogService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            if (await authService.IsUserAuthenticated())
                Interceptor.RegisterEvent();

        
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await broadCastService.StartAsync(hubConnection);

            if (firstRender)
            {
                hubConnection.Remove(BroadCastType.Message);
                SubscribeBroadCastChat(broadCastType: BroadCastType.Message,
                   async chat =>
                   {
                       Console.WriteLine(chat.Message);
                       Console.WriteLine(chat.Data);

                       await ReceiveAsync(chat.Message, AppExtensions.TimeAgo(chat.Created), chat.Avatar, chat.Data);
                       toastService.ShowInfo(chat.Message == "{{html}}" ? "Thông tin đơn hàng" : chat.Message, "Có một tin nhắn mới", OpenChat);
                       await jSRuntime.InvokeVoidAsync("scrollToBottom", "#chat-user");
                       StateHasChanged();
                   });
            }

        }
        public async void OpenChat()
        {

            await DialogService.OpenAsync<PoPoy.Client.Pages.Chat.Chat>("Hộp thư",
                     options: new DialogOptions() { Width = "1200px", Height = "712px", Resizable = true, Draggable = true, CssClass = "modal-content", CloseDialogOnOverlayClick = true });

        }

        private async Task ReceiveAsync(string message, string time, string avt, string data = null)
        {
            await jSRuntime.InvokeVoidAsync("receiveChat", message, time, avt, data);

        }

        private void SubscribeBroadCastChat(string broadCastType, Action<ChatDto> action)
        {
            hubConnection.On<ChatDto>(broadCastType, action);
        }
    }
}
