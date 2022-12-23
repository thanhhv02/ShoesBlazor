using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.BroadCastService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.Enum;
using PoPoy.Shared.ViewModels;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PoPoy.Api.Hubs
{
    [AuthorizeToken]

    public class AppHub : Hub
    {

        private readonly IBroadCastService broadCastService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        
        public AppHub(IBroadCastService broadCastService, IMapper mapper, UserManager<User> userManager)
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
        public async Task ReadChat(ReadChatDto chatDto)
        {
            await broadCastService.ReadMessage(chatDto.ReceiverId,chatDto.SenderId);
        }
        public async Task<NotificationDto> SendNotiAllAdmin(CreateOrUpdateNotiDto notiDto)
        {
            var user = Context.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var notification = mapper.Map<NotificationDto>(notiDto);
            notification.SenderId = Guid.Parse(userId);
            await broadCastService.SendNotifyAllAdmin(notification);
            return notification;
        }

        public async Task<NotificationDto> SendNotiUserId(CreateOrUpdateNotiDto notiDto)
        {
            var notification = mapper.Map<NotificationDto>(notiDto);
            await broadCastService.SendNotifyUserId(notification);
            return notification;
        }
        public async Task ReadNoti(Guid notiId)
        {

            await broadCastService.ReadNoti(notiId);
        }
        public async Task ReadAllNoti(Guid userId)
        {

            await broadCastService.ReadAllNoti(userId);
        }


    }
}
