using AutoMapper;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using System.Collections.Generic;

namespace PoPoy.Api.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrUpdateNotiDto, NotificationDto>();
            CreateMap<NotificationDto, Notification>();
            CreateMap<CreateOrUpdateChatDto, ChatDto>();
            CreateMap<ChatDto, Chat>();

        }
    }
}
