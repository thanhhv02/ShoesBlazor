using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.Enum;
using System.Threading.Tasks;

namespace PoPoy.Api.Hubs
{
    [Authorize]

    public class ChatHub : Hub
    {

        public async Task SendMessage(ChatDto chatDto)
        {
            await Clients.User(chatDto.ReceiverId.ToString()).SendAsync(BroadCastType.Message , chatDto);
        }
    }
}
