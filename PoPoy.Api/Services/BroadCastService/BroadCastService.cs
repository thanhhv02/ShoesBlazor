using Microsoft.AspNetCore.SignalR;
using PoPoy.Api.Data;
using PoPoy.Api.Hubs;
using PoPoy.Shared.Enum;
using PoPoy.Shared.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoPoy.Api.Services.NotificationService;
using PoPoy.Api.Services.ChatService;

namespace PoPoy.Api.Services.BroadCastService
{
    public class BroadCastService : IBroadCastService
    {
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly IHubContext<ChatHub> chatHubContext;
        private readonly INotificationService notificationService;
        private readonly IChatService chatService;



        public BroadCastService(IHubContext<NotificationHub> hubContext, IHubContext<ChatHub> chatHubContext, INotificationService notificationService, IChatService chatService)
        {
            this.hubContext = hubContext;
            this.chatHubContext = chatHubContext;
            this.notificationService = notificationService;
            this.chatService = chatService;
        }



        public async Task ReadNoti(Guid id)
        {
            await notificationService.ReadNoti(id);
        }

        public async Task ReadAllNoti(Guid userId)
        {
            await notificationService.ReadAllNoti(userId);
        }
        public async Task SendNotifyAll(NotificationDto notification)
        {
            notification.Created = DateTime.Now;
            notification.IsRead = false;
            await notificationService.CreateNotification(notification);
            await hubContext.Clients.All.SendAsync(BroadCastType.Notify, notification);
        }

        public async Task SendNotifyAllAdmin(NotificationDto notification)
        {
            // gui thong bao cho tat ca cac nguoi admin
            notification.Created = DateTime.Now;
            notification.IsRead = false;
            var userIds = await notificationService.CreateNotificationAllAdmin(notification);
            await hubContext.Clients.Users(userIds).SendAsync(BroadCastType.Notify, notification);
        }

        public async Task SendNotifyUserId(NotificationDto notification)
        {
            // gui thong bao cho user Id
            notification.Created = DateTime.Now;
            notification.IsRead = false;
            await notificationService.CreateNotificationUserId(notification);
            await hubContext.Clients.User(notification.UserId.ToString()).SendAsync(BroadCastType.Notify, notification);
        }


        public async Task SendMessage(ChatDto chatDto)
        {
            chatDto.Created = DateTime.Now;
            chatDto.IsRead = false;
            await chatService.CreateChat(chatDto);
            await chatHubContext.Clients.User(chatDto.ReceiverId.ToString()).SendAsync(BroadCastType.Message, chatDto);
        }

    }
}
