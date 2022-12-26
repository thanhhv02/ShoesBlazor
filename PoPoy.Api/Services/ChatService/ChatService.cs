using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoPoy.Api.Data;
using PoPoy.Api.Extensions;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.ChatService
{
    public class ChatService : IChatService
    {

        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public ChatService(DataContext dataContext, IMapper mapper, UserManager<User> userManager)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<bool> CreateChatUserId(ChatDto chatDto)
        {
            var chat = mapper.Map<Chat>(chatDto);
            chat.Created = DateTime.UtcNow.ToLocalTime();
            chat.IsRead = false;
            await dataContext.Chats.AddAsync(chat);
            return  await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyList<string>> CreateChatAllAdmin(ChatDto chatDto)
        {
            // gửi cho userId thuoc admin va staff
            var listUser = new List<User>();
            var userAdminIds = await userManager.GetUsersInRoleAsync(RoleName.Admin);
            listUser.AddRange(userAdminIds);
            listUser = listUser.DistinctBy(p => p.Id).ToList();
            var chats = new List<Chat>();
            foreach (var id in listUser.Select(p => p.Id))
            {
                var chat = mapper.Map<Chat>(chatDto);
                chat.ReceiverId = id;
                chats.Add(chat);
            }
            await dataContext.Chats.AddRangeAsync(chats);
            await dataContext.SaveChangesAsync();
            return listUser.Select(p => p.Id.ToString()).ToList();
        }

        public async Task ReadMessage(Guid receiverId , Guid senderId)
        {
            var list = await dataContext.Chats.Where(p => p.ReceiverId == receiverId && p.SenderId == senderId).ToListAsync();
            list.ForEach(p => p.IsRead = true);
             dataContext.UpdateRange(list);
            await dataContext.SaveChangesAsync();
        }

        public async Task<List<ListChatSender>> GetListChatSender(Guid userId)
        {
            var list = await dataContext.Chats.Include(p => p.Sender).Where(p => p.ReceiverId == userId).Where(p => p.SenderId != p.ReceiverId).ToListAsync();
            List<ListChatSender> chats = list.GroupBy(p => p.Sender, p => p, (sender, chat) =>

            new ListChatSender { Id = Guid.NewGuid(), User = new UserChat
            {
                UserId = sender.Id,
                AvatarPath = sender.AvatarPath,
                FullName = sender.FirstName + " " + sender.LastName,
                CountUnRead = chat.Count(p => p.IsRead == false),
                IsRead = !chat.Any(p => p.IsRead == false),

            },
                SenderChats = chat.Select(c => new ChatDto
                {
                    IsMe = false,
                    Id = c.Id,
                    Created = c.Created,
                    Message = c.Message,
                    IsRead = c.IsRead,
                    Data = c.data
                }).ToList(),
                ReceiverChats = dataContext.Chats.Where(p => p.SenderId == userId && p.ReceiverId == sender.Id).Select(u => new ChatDto
                {
                    Id = u.Id,
                    IsMe = true,
                    Created = u.Created,
                    Message = u.Message,
                    IsRead = u.IsRead,
                    Data = u.data
                }).ToList()

            }).ToList();
            return chats;
        }
        public async Task<List<ListChatUser>> GetListChatUser(Guid userId)
        {

            var model = await dataContext.Chats.Include(p => p.Sender).Include(p => p.Receiver).Where(p => (p.SenderId == userId) || p.ReceiverId == userId).ToListAsync();
            var list = model.Select(delegate(Chat p) {
                var listchat = new ListChatUser();
                var chat = new ChatDto();
                chat = mapper.Map<ChatDto>(p);
                if (chat.SenderId == userId)
                {
                    chat.IsMe = true;
                        listchat.UserChat = new UserChat
                        {
                            UserId = p.Sender.Id,
                            AvatarPath = p.Sender.AvatarPath,
                            FullName = p.Sender.FirstName + " " + p.Sender.LastName,
                        };
                    }
                else
                {
                        chat.IsMe = false;

                        listchat.UserChat = new UserChat
                    {
                        UserId = p.Sender.Id,
                        AvatarPath = p.Sender.AvatarPath,
                        FullName = p.Sender.FirstName + " " + p.Sender.LastName,
                    };

                }
                listchat.Chat = chat;
                return listchat;
            }).ToList();
            list = list.DistinctBy(p => p.Chat.Id).ToList();
            list = list.OrderBy(p => p.Chat.Created).ToList() ;
            return list;
        }

    }
}
