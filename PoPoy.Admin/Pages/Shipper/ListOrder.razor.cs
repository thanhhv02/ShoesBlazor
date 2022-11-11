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
    public partial class ListOrder
    {
        [Inject] private IOrderService orderService { get; set; }
        [Inject] private IAuthService authService { get; set; }
        [Inject] private IBroadCastService broadCastService { get; set; }
        [Inject] private IToastService toastService { get; set; }



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
            OrderShipperSearchDto input = new();
            input.Status = OrderStatus.Delivering;
            input.ShipperId = userId;

            orders = await orderService.GetOrderByShipper(input);
            StateHasChanged();
        }

        private async Task UpdateStatus(string orderId, OrderStatus orderStatus, Guid userId, string currentName)
        {
            UpdateStatusOrderDto input = new UpdateStatusOrderDto()
            {
                OrderId = orderId,
                OrderStatus = orderStatus
            };
            var result = await orderService.UpdateStatusOrder(input);
            var noti = orderStatus == OrderStatus.Delivered ? $"Đơn hàng {orderId} của bạn đã được giao thành công" : $"Đơn hàng: {orderId} | Giao thất bại";

            if (result)
            {
                NotiSendConfig notiSend = new NotiSendConfig("Cập nhật trạng thái đơn hàng", noti, $"/order/{orderId}");
                await broadCastService.SendNotiAllAdmin(notiSend);
                await broadCastService.SendNotiUserId(notiSend, userId);
                toastService.ShowSuccess("Cập nhật thành công");
            }
            else
            {
                toastService.ShowSuccess("Cập nhật thất bại");

            }
            await LoadOrder();
            StateHasChanged();
        }
    }
}
