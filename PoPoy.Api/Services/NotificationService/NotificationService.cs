using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoPoy.Api.Data;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public NotificationService(DataContext dataContext, IMapper mapper, UserManager<User> userManager)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task ReadNoti(Guid notiId)
        {
            var noti = await dataContext.Notifications.FindAsync(notiId);
            if (noti is not null)
            {
                noti.IsRead = true;
                dataContext.Update(noti);
                await dataContext.SaveChangesAsync();
            }
        }

        public async Task ReadAllNoti(Guid userId)
        {
            var notis = await dataContext.Notifications.Where(p => p.UserId == userId).ToListAsync();

            if (notis is not null)
            {
                notis.ForEach(i => i.IsRead = true);
                dataContext.UpdateRange(notis);
                await dataContext.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<string>> CreateNotificationAllAdmin(NotificationDto notiDto)
        {
            // gửi cho userId thuoc admin va staff
            var listUser = new List<User>();
            var userAdminIds = await userManager.GetUsersInRoleAsync(RoleName.Admin);
            var userStaffIds = await userManager.GetUsersInRoleAsync(RoleName.Staff);
            listUser.AddRange(userAdminIds);
            listUser.AddRange(userStaffIds);
            var notis = new List<Notification>();
            foreach (var id in listUser.Select(p => p.Id))
            {
                var noti = mapper.Map<Notification>(notiDto);
                noti.UserId = id;
                notis.Add(noti);
            }
            await dataContext.Notifications.AddRangeAsync(notis);
            await dataContext.SaveChangesAsync();
            return listUser.Select(p => p.Id.ToString()).ToList();
        }

        public async Task<bool> CreateNotificationUserId(NotificationDto notiDto)
        {
            // gửi cho userId 

            var noti = mapper.Map<Notification>(notiDto);
            await dataContext.Notifications.AddAsync(noti);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateNotification(NotificationDto notiDto)
        {
            var notification = mapper.Map<Notification>(notiDto);
            await dataContext.Notifications.AddAsync(notification);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Guid> DeleteNotification(Guid id)
        {
            var notification = await dataContext.Notifications.FindAsync(id);
            dataContext.Remove(notification);
            await dataContext.SaveChangesAsync();
            return id;
        }

        public async Task<List<NotificationDto>> GetAllNotifications()
        {
            return mapper.Map<List<NotificationDto>>(await dataContext.Notifications.ToListAsync());
        }
        public async Task<List<NotificationDto>> GetAllNotificationsByUserId(Guid id)
        {
            var list = new List<NotificationDto>();
            list = await dataContext.Notifications.Include(p => p.User).Where(p => p.UserId == id)
                .Select(p => new NotificationDto()
                {
                    UserId = p.UserId,
                    Id = p.Id,
                    Created = p.Created,
                    Title = p.Title,
                    Message = p.Message,
                    Data = p.Data,
                    DataUrl = p.DataUrl,
                    User = p.User,
                    IsRead = p.IsRead
                }).ToListAsync();
            return list;
        }


        public async Task<NotificationDto> GetNotificationById(Guid id)
        {
            return mapper.Map<NotificationDto>(await dataContext.Notifications.FindAsync(id));

        }

        public async Task<bool> UpdateNotification(NotificationDto notiDto)
        {
            var notiOld = await dataContext.Notifications.FindAsync(notiDto.Id);

            var notiNew = mapper.Map<Notification>(notiOld);

            dataContext.Update(notiNew);
            return await dataContext.SaveChangesAsync() > 0;

        }
    }
}
