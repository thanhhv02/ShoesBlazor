using AutoMapper;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.ViewModels;
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
            CreateMap<Chat, ChatDto>();


            CreateMap<ProductSize, ProductSizeDto>();
            CreateMap<ProductSizeDto, ProductSize>();

            CreateMap<ProductColor, ProductColorDto>();
            CreateMap<ProductColorDto, ProductColor>();

        }
    }
}
