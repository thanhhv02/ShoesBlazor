using AutoMapper;
using PoPoy.Shared.Dto;
using System.Collections.Generic;

namespace PoPoy.Api.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrUpdateNotiDto, NotificationDto>();
            CreateMap<NotificationDto, Notification>();


        }
    }
}
