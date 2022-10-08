using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using PoPoy.Api.Services.BroadCastService;
using PoPoy.Api.Services.NotificationService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PoPoy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class BroadCastController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IBroadCastService broadCastService;
        private readonly INotificationService notificationService;

        private readonly IMapper mapper;

        public BroadCastController(UserManager<User> userManager, IBroadCastService broadCastService, INotificationService notificationService, IMapper mapper)
        {
            this.userManager = userManager;
            this.broadCastService = broadCastService;
            this.notificationService = notificationService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendNotiAllAdmin(CreateOrUpdateNotiDto notiDto)
        {
      
            var notification =  mapper.Map<NotificationDto>(notiDto);
            await broadCastService.SendNotifyAllAdmin(notification);
            return Ok(notification);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendNotiUserId(CreateOrUpdateNotiDto notiDto)
        {
            var notification = mapper.Map<NotificationDto>(notiDto);
            await broadCastService.SendNotifyUserId(notification);
            return Ok(notification);
        }


        [HttpPost]
        [Authorize]

        public async Task<IActionResult> ReadNoti(Guid notiId)
        {

            await broadCastService.ReadNoti(notiId);
            return Ok();
        }
        [HttpPost]

        public async Task<IActionResult> ReadAllNoti(Guid userId)
        {

            await broadCastService.ReadAllNoti(userId);
            return Ok();
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetAllNotiByUserId(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Thiếu trường ID");
            }
           
            var notifications = await notificationService.GetAllNotificationsByUserId(id);

            var result = new ServiceResponse<List<NotificationDto>>();
            
           result.Message = "Lấy danh sách thông báo cho User thành công";
           result.Success = true;
           result.Data = notifications;
         
          
            return Ok(result);
        }
    }
}
