using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.ChatService
{
    public interface IChatService
    {
        Task<bool> CreateChatUserId(ChatDto chatDto);

        Task<IReadOnlyList<string>> CreateChatAllAdmin(ChatDto chatDto);

        Task<List<ListChatSender>> GetListChatSender(Guid userId);
        Task<List<ListChatUser>> GetListChatUser(Guid userId);
        Task ReadMessage(Guid receiverId, Guid senderId);
    }
}
