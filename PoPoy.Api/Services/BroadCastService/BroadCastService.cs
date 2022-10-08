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

namespace PoPoy.Api.Services.BroadCastService
{
    public class BroadCastService : IBroadCastService
    {
        private readonly IHubContext<NotificationHub> hubContext;

        private readonly INotificationService notificationService;

        public BroadCastService(IHubContext<NotificationHub> hubContext, INotificationService notificationService)
        {
            this.hubContext = hubContext;
            this.notificationService = notificationService;
        }

        public async Task BroadCastNotifyUser(ChatDto broadCast)
        {
            await hubContext.Clients.User(broadCast.Receiver.ToString()).SendAsync(BroadCastType.Notify, broadCast.Message);
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
            await notificationService.CreateNotificationAllAdmin(notification);
            await hubContext.Clients.All.SendAsync(BroadCastType.Notify, notification);
        }

        public async Task SendNotifyUserId(NotificationDto notification)
        {
            // gui thong bao cho user Id
            notification.Created = DateTime.Now;
            notification.IsRead = false;
            await notificationService.CreateNotificationUserId(notification);
            await hubContext.Clients.User(notification.UserId.ToString()).SendAsync(BroadCastType.Notify, notification);
        }

        public async Task BroadCastMessageUser(string userId)
        {
            await hubContext.Clients.User(userId).SendAsync(BroadCastType.Message, "");
        }
    }
}
