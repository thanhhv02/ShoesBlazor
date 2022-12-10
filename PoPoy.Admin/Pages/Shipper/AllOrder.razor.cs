using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Entities.OrderDto;
using PoPoy.Shared.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PoPoy.Admin.Services.OrderService;
using PoPoy.Admin.Services.BroadCastService;
using PoPoy.Admin.Services.AuthService;

namespace PoPoy.Admin.Pages.Shipper
{
    public partial class AllOrder
    {
        [Inject] private IOrderService orderService { get; set; }
        [Inject] private IAuthService authService { get; set; }



        private List<Order> orders = new();
        private Guid userId;
        protected override async Task OnInitializedAsync()
        {
            userId = await authService.GetUserId();
            await LoadOrder();
            StateHasChanged();

        }

        private async Task LoadOrder()
        {
            orders = await orderService.GetOrderHistoryShipper();
            StateHasChanged();
        }

       
    }
}
