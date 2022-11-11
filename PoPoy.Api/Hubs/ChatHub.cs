using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.Enum;
using System;
using System.Threading.Tasks;

namespace PoPoy.Api.Hubs
{
    [Authorize]

    public class ChatHub : Hub
    {

        public async Task SendMessageUserId(ChatDto chatDto)
        {
            await Clients.User(chatDto.ReceiverId.ToString()).SendAsync(BroadCastType.Message , chatDto);
        }

        public async Task SendMessageAllAdmin(ChatDto chatDto)
        {
            await Clients.Users(chatDto.UserIds).SendAsync(BroadCastType.Message, chatDto);
        }

        public override Task OnConnectedAsync()
        {
            if (UserHandler.UserListChat.Count == 0)
            UserHandler.UserListChat.Add(Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.UserListChat.RemoveAll(u => u == Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
