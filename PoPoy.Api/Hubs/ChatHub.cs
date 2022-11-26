using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.BroadCastService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.Enum;
using System;
using System.Threading.Tasks;

namespace PoPoy.Api.Hubs
{
    [AuthorizeToken]

    public class ChatHub : Hub
    {

        private readonly IBroadCastService broadCastService;
        private readonly IMapper mapper;

        public ChatHub(IBroadCastService broadCastService, IMapper mapper)
        {
            this.broadCastService = broadCastService;
            this.mapper = mapper;
        }

        public async Task SendMessageUserId(CreateOrUpdateChatDto chatDto)
        {
            var chat = mapper.Map<ChatDto>(chatDto);
            await broadCastService.SendMessageUserId(chat);

        }

        public async Task SendMessageAllAdmin(CreateOrUpdateChatDto chatDto)
        {
            var chat = mapper.Map<ChatDto>(chatDto);
            await broadCastService.SendMessageAllAdmin(chat);
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
