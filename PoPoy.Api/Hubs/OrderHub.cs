using Microsoft.AspNetCore.SignalR;
using PoPoy.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Hubs
{
    [AuthorizeToken]
    public class OrderHub : Hub
    {
    }
}
