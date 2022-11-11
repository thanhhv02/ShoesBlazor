using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using PoPoy.Client.Services.BroadCastService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.Enum;
using Syncfusion.Blazor.Gantt.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Client.Shared.PopupChat
{
    public partial class PopupChat
    {
        [Inject] private IJSRuntime jSRuntime { get; set; }
        [Inject] public IBroadCastService broadCastService { get; set; }
        private HubConnection hubConnection { get; set; }
        private string currentUserIdChat = string.Empty; 
        private bool showChat = false;
        private string Message = string.Empty;
        private string AvatarPath ;

        private List<ListChatUser> ListChat = new();
        protected override async Task OnInitializedAsync()
        {
      
            hubConnection = await broadCastService.BuidHubWithToken(BroadCastType.Message);
            SubscribeBroadCastChat(broadCastType: BroadCastType.Message,
                async chat =>
                {
                    if (chat.SenderId == Guid.Parse(currentUserIdChat))
                    {
                        await jSRuntime.InvokeVoidAsync("sendChat", chat.Message, chat.Created.ToString("HH:mm"), AvatarPath);

                    }
                    else
                    {
                        await ReceiveAsync(chat.Message, chat.Created.ToString("HH:mm"));

                    }
                    await ScrollToBottom();
                    StateHasChanged();
                });
            await broadCastService.StartAsync(hubConnection);

            await LoadDataAsync();

            currentUserIdChat = await broadCastService.GetUserIdCurrentChat();
            string path = await broadCastService.GetUserAvtChat();
            AvatarPath = string.IsNullOrEmpty(path) ? "/images/avatar.jpg" : path;
            StateHasChanged();
        }
        private void OpenChat()
        {

            showChat = true;
            StateHasChanged();
        }
        private void CloseChat()
        {
            showChat = false;
            StateHasChanged();
        }

        private async Task ScrollToBottom()
        {
            await Task.Delay(200);
            await jSRuntime.InvokeVoidAsync("scrollToBottom", "#chat-user");
        }

        private async Task SendAsync()
        {

            if (!string.IsNullOrEmpty(Message))
            {
                await jSRuntime.InvokeVoidAsync("sendChat", Message, DateTime.Now.ToString("HH:mm") , AvatarPath);
                await broadCastService.SendMessageAllAdmin(Message);
                Message = string.Empty;

            }
        }

        private async Task ReceiveAsync(string message, string time)
        {
            await jSRuntime.InvokeVoidAsync("receiveChat", message, time);

        }

     

        private void SubscribeBroadCastChat(string broadCastType, Action<ChatDto> action)
        {
            hubConnection.On<ChatDto>(broadCastType, action);
        }

        private async Task LoadDataAsync()
        {
            var result = await broadCastService.GetListChatUser();
           
            ListChat = result.Data;

        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await ScrollToBottom();

        }

    }
}
