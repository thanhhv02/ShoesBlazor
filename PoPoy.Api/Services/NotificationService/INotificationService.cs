using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.NotificationService
{
    public interface INotificationService
    {

        Task<List<NotificationDto>> GetAllNotifications();
        Task<NotificationDto> GetNotificationById(Guid id);
        Task<bool> CreateNotification(NotificationDto notiDto);
        Task<bool> CreateNotificationAllAdmin(NotificationDto notiDto);
        Task<bool> CreateNotificationUserId(NotificationDto notiDto);
        Task<bool> UpdateNotification(NotificationDto notiDto);
        Task<List<NotificationDto>> GetAllNotificationsByUserId(Guid id);

        Task<Guid> DeleteNotification(Guid id);

        Task ReadNoti(Guid notiId);
        Task ReadAllNoti(Guid userId);
    }
}
