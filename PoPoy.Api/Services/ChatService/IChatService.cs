using PoPoy.Shared.Dto;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.ChatService
{
    public interface IChatService
    {
        Task<bool> CreateChat(ChatDto chatDto);
    }
}
