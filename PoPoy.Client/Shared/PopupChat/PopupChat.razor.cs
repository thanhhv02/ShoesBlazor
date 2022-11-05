using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using PoPoy.Client.Services.BroadCastService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using Syncfusion.Blazor.Gantt.Internal;
using System;
using System.Threading.Tasks;

namespace PoPoy.Client.Shared.PopupChat
{
    public partial class PopupChat
    {
        [Inject] private IJSRuntime jSRuntime { get; set; }
        [Inject] public IBroadCastService broadCastService { get; set; }
        private HubConnection hubConnection { get; set; }

        private bool showChat = false;
        private string Message = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = await broadCastService.BuidHubWithToken(BroadCastType.Message);
            SubscribeBroadCastChat(broadCastType: BroadCastType.Message,
                async chat =>
                {
                    Console.WriteLine("THANHNE");
                    await ReceiveAsync(chat.Message, chat.Created.ToString("HH:mm"));
                    await ScrollToBottom();
                    StateHasChanged();
                });
            await broadCastService.StartAsync(hubConnection);
            await ScrollToBottom();
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
                await jSRuntime.InvokeVoidAsync("sendChat", Message, DateTime.Now.ToString("HH:mm"));
                Message = string.Empty;

            }
        }

        private async Task ReceiveAsync(string message, string time)
        {
            await jSRuntime.InvokeVoidAsync("receiveChat", message, time);

        }

        public async Task EnterSendChat(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await SendAsync();
            }
        }

        private void SubscribeBroadCastChat(string broadCastType, Action<ChatDto> action)
        {
            hubConnection.On<ChatDto>(broadCastType, action);
        }
    }
}
