using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Hubs
{
    [Authorize]
    public class SignalHub : Hub
    {  

        public async Task BroadCastNotifyUser(ChatDto broadCast)
        {
            await Clients.User(broadCast.Receiver.ToString()).SendAsync(BroadCastType.Notify, broadCast);
        }

        public async Task BroadCastNotifyAll(ChatDto broadCast)
        {
            // gui thong bao cho tat ca cac nguoi admin


            await Clients.All.SendAsync(BroadCastType.Notify, broadCast);
        }

        public async Task BroadCastMessageUser(string userId)
        {
            await Clients.User(userId).SendAsync(BroadCastType.Message, "");
        }
    }
}
