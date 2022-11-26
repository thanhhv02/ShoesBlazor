using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.BroadCastService;
using PoPoy.Api.Services.NotificationService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System;
using System.Threading.Tasks;

namespace PoPoy.Api.Hubs
{
    [AuthorizeToken]

    public class NotificationHub : Hub
    {
   
    

    }
}
