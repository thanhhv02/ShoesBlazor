using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.BroadCastService
{
    public interface IBroadCastService
    {

        Task SendNotifyAll(NotificationDto notification);
        Task SendNotifyAllAdmin(NotificationDto notification);
        Task SendNotifyUserId(NotificationDto notification);
        Task ReadNoti(Guid id);
        Task ReadAllNoti(Guid userId);

        Task SendMessageUserId(ChatDto chatDto);
        Task SendMessageAllAdmin(ChatDto chatDto);
        Task ReadMessage(Guid receiverId, Guid senderId);
        Task<List<ListChatSender>> GetListChatSender(Guid userId);
        Task<List<ListChatUser>> GetListChatUser(Guid userId);
        Task SendOrderForShipper(Guid shipperId);
    }
}
