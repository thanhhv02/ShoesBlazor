using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PoPoy.Api.Data;
using PoPoy.Shared.Dto;
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

        public async Task<bool> CreateChat(ChatDto chatDto)
        {
            var chat = mapper.Map<Chat>(chatDto);
            chat.Created = DateTime.Now;
            chat.IsRead = false;
            await dataContext.Chats.AddAsync(chat);
            return  await dataContext.SaveChangesAsync() > 0;
        }

    }
}
