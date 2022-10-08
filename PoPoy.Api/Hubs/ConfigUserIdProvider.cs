using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;

namespace PoPoy.Api.Hubs
{
    public class ConfigUserIdProvider : IUserIdProvider
    {

        public virtual string GetUserId(HubConnectionContext connectionContext)
        {
            return connectionContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }
    }
}
