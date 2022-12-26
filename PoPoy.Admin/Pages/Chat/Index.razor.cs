using Blazored.Toast.Services;
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
        [Inject] private IToastService toastService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }

        private List<ListChatSender> ListChatSenders = new();
        private ListChatSender Current;
        private List<ChatDto> ChatDtos = new List<ChatDto>();
        [Inject] private HubConnection hubConnection { get; set; }
        private string hostClient;
        private string Message = string.Empty;
        private string Avatar = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            broadCastService.SetHub(hubConnection);
            Avatar = await broadCastService.GetUserAvtChat();
            Console.WriteLine("chat");
            broadCastService.SetHub(hubConnection);
           

            var cr = ListChatSenders.FirstOrDefault();
            if (cr != null) await GetUserChat(cr.User.UserId);
            await ScrollToBottom();
            StateHasChanged();

        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SubscribeBroadCastChat(broadCastType: BroadCastType.Message,
               async chat =>
               {
                   chat.IsMe = false;
                   if (chat.SenderId == Current.User.UserId)
                   {
                       await ReceiveAsync(chat.Message, AppExtensions.TimeAgo(chat.Created), chat.Avatar, chat.Data);
                       await jSRuntime.InvokeVoidAsync("sendChatmini2", chat.Message, chat.SenderId);
                       Current.SenderChats.Add(chat);
                   }
                   else
                   {

                       await jSRuntime.InvokeVoidAsync("sendChatmini", chat.Message, chat.SenderId);

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

            }
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

        private async Task GetUserChat(Guid? id)
        {
            var chats = new List<ChatDto>();
            ChatDtos = new List<ChatDto>();
            Current = ListChatSenders.FirstOrDefault(p => p.User.UserId == id);
            chats = new List<ChatDto>(Current.SenderChats);
      


            chats.AddRange(Current.ReceiverChats);
            ChatDtos = chats.OrderBy(p => p.Created).ToList();
            await broadCastService.ReadChat(Current.User.UserId.GetValueOrDefault());

            StateHasChanged();
            await ScrollToBottom();
            await jSRuntime.InvokeVoidAsync("clearChat");
            await jSRuntime.InvokeVoidAsync("hideCount" , Current.User.UserId);

        }

        private async Task SelectUserChat(Guid? id)
        {

            var result = await broadCastService.GetListChatSender();
            ListChatSenders = result.Data;
            await GetUserChat(id);  
        }


        private async Task ScrollToBottom()
        {
           
            await jSRuntime.InvokeVoidAsync("scrollToBottom", "#chat-user");
        }

        private async Task SendAsync()
        {

            if (!string.IsNullOrEmpty(Message))
            {
                await jSRuntime.InvokeVoidAsync("sendChat", Message, AppExtensions.TimeAgo(DateTime.UtcNow.ToLocalTime()), Avatar);
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
