﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PoPoy.Client.Services.BroadCastService;
using Blazored.Toast.Services;
using Radzen;
using PoPoy.Client.Services;

namespace PoPoy.Client.Pages.Chat
{
    public partial class Chat
    {
        [Inject] private IJSRuntime jSRuntime { get; set; }
        [Inject] private IBroadCastService broadCastService { get; set; }
        [Inject] private IToastService toastService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private DialogService dialogService { get; set; }

        private HubConnection hubConnection { get; set; }
        private string currentUserIdChat = string.Empty;
        private string Message = string.Empty;
        private string AvatarPath;

        private List<ListChatUser> ListChat = new();
        protected override async Task OnInitializedAsync()
        {

            hubConnection = await broadCastService.BuidHubWithToken(BroadCastType.Message);
            await hubConnection.StartAsync();

            SubscribeBroadCastChat(broadCastType: BroadCastType.Message,
                async chat =>
                {
                    if (chat.SenderId == Guid.Parse(currentUserIdChat))
                    {
                        await jSRuntime.InvokeVoidAsync("sendChat", chat.Message, chat.Created.ToString("HH:mm"), AvatarPath , chat.Data);

                    }
                    else
                    {
                        await ReceiveAsync(chat.Message, chat.Created.ToString("HH:mm") , chat.Avatar);

                    }
                    await ScrollToBottom();
                    StateHasChanged();
                });

            await LoadDataAsync();

            currentUserIdChat = await broadCastService.GetUserIdCurrentChat();
            string path = await broadCastService.GetUserAvtChat();
            AvatarPath = string.IsNullOrEmpty(path) ? "/images/avatar.jpg" : path;
            StateHasChanged();
        }
   
        private async void OpenChat()
        {
            await dialogService.OpenAsync<Chat>("Hộp thư", options: new DialogOptions() { Width = "1200px", Height = "712px", Resizable = true, Draggable = true, CssClass = "modal-content", CloseDialogOnOverlayClick = true });

        }
        private void CloseChat()
        {
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
                await jSRuntime.InvokeVoidAsync("sendChat", Message, DateTime.Now.ToString("HH:mm"), AvatarPath);
                CreateOrUpdateChatDto model = new() { Data = null, Message = Message, Avatar =  AvatarPath, SenderId = Guid.Parse(currentUserIdChat) };
                // var resp = await httpClient.PostAsync($"/api/BroadCast/SendMessageAllAdmin", model.ToJsonBody());
                await broadCastService.SendMessageAllAdmin(hubConnection, message: Message);
                Message = string.Empty;

            }
        }

        private async Task ReceiveAsync(string message, string time , string avt, string data = null)
        {
            await jSRuntime.InvokeVoidAsync("receiveChat", message, time , avt , data);
             
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
