using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using PoPoy.Admin.Extensions;
using PoPoy.Admin.Services.BroadCastService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Admin.Pages.Chat
{
    public partial class Index
    {
        [Inject] IBroadCastService broadCastService { get; set; }
        [Inject] IConfiguration configuration { get; set; }
        [Inject] private IJSRuntime jSRuntime { get; set; }

        private List<ListChatSender> ListChatSenders = new();
        private ListChatSender Current;
        private List<ChatDto> ChatDtos = new List<ChatDto>();
        private HubConnection hubConnection { get; set; }
        private string hostClient;
        private string Message = string.Empty;
        private string Avatar = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            var cr = ListChatSenders.FirstOrDefault();
            if (cr != null) SelectUserChat(cr.Id);
            StateHasChanged();
            Avatar = await broadCastService.GetUserAvtChat();
            hubConnection = await broadCastService.BuidHubWithToken(BroadCastType.Message);
            SubscribeBroadCastChat(broadCastType: BroadCastType.Message,
                async chat =>
                {
                    chat.IsMe = false;
                    if (chat.SenderId == Current.User.UserId)
                    {
                        await ReceiveAsync(chat.Message, chat.Created.ToString("HH:mm"), chat.Avatar, chat.Data);
                        await jSRuntime.InvokeVoidAsync("sendChatmini2", chat.Message, chat.SenderId);
                        Current.SenderChats.Add(chat);
                        await ScrollToBottom();
                    }
                    else
                    {

                        await  jSRuntime.InvokeVoidAsync("sendChatmini", chat.Message, chat.SenderId);

                        foreach (var item in ListChatSenders)
                        {
                            if (item.User.UserId == chat.SenderId)
                            {
                                item.SenderChats.Add(chat);
                                break;

                            }
                        }
                    }

                    StateHasChanged();


                });
            await broadCastService.StartAsync(hubConnection);
            await ScrollToBottom();
        }
        private async Task LoadDataAsync()
        {
            var result = await broadCastService.GetListChatSender();
            ListChatSenders = result.Data;
            hostClient = configuration["ClientUrl"];
        }
        private async Task Search(string str)
        {
            await LoadDataAsync();
            var list = ListChatSenders;
            if (!string.IsNullOrEmpty(str))
            {
                list = ListChatSenders.Where(c => c.User.FullName.ConvertToUnSign().Contains(str.ConvertToUnSign())).ToList();
            }
            ListChatSenders = list;
            await InvokeAsync(StateHasChanged);
        }

        private async Task SelectUserChat(Guid id)
        {
       
            var chats = new List<ChatDto>();
            ChatDtos = new List<ChatDto>();
            Current = ListChatSenders.FirstOrDefault(p => p.Id == id);
            chats = new List<ChatDto>(Current.SenderChats);
      


            chats.AddRange(Current.ReceiverChats);
            ChatDtos = chats.OrderBy(p => p.Created).ToList();
            await broadCastService.ReadChat(Current.User.UserId.GetValueOrDefault());

            StateHasChanged();
            await ScrollToBottom();
            await jSRuntime.InvokeVoidAsync("clearChat");
            await jSRuntime.InvokeVoidAsync("hideCount" , Current.User.UserId);

        }


        private async Task ScrollToBottom()
        {
           
            await jSRuntime.InvokeVoidAsync("scrollToBottom", "#chat-user");
        }

        private async Task SendAsync()
        {

            if (!string.IsNullOrEmpty(Message))
            {
                await jSRuntime.InvokeVoidAsync("sendChat", Message, DateTime.Now.ToString("HH:mm"), Avatar);
                await broadCastService.SendMessageUserId(Message , Current.User.UserId);
                await jSRuntime.InvokeVoidAsync("sendChatmini2", Message, Current.User.UserId);

                Message = string.Empty;

            }
        }

        private async Task ReceiveAsync(string message, string time , string avt, string data)
        {
            await jSRuntime.InvokeVoidAsync("receiveChat", message, time , avt, data);

        }



        private void SubscribeBroadCastChat(string broadCastType, Action<ChatDto> action)
        {
            hubConnection.On<ChatDto>(broadCastType, action);
        }
    }
}
