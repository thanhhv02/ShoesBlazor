using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System.Threading.Tasks;

namespace PoPoy.Api.Hubs
{
    [Authorize]

    public class NotificationHub : Hub
    {
        public async Task SendNotifyAllAdmin(NotificationDto notification)
        {
            // gui thong bao cho tat ca cac nguoi admin


            await Clients.All.SendAsync(BroadCastType.Notify, notification);
        }

        public async Task SendNotifyUserId(NotificationDto notification)
        {

            await Clients.User(notification.UserId.ToString()).SendAsync(BroadCastType.Notify, notification);
        }


    }
}
