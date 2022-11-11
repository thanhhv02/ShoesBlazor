using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using PoPoy.Admin.Services.AuthService;
using PoPoy.Admin.Services.BroadCastService;
using PoPoy.Admin.Services.OrderService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Entities.OrderDto;
using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Admin.Pages.Shipper
{
    public partial class Index
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
            input.Status = PoPoy.Shared.Enum.OrderStatus.Confirmed;
            input.ShipperId = userId;

            orders = await orderService.GetOrderByShipper(input);
            StateHasChanged();
        }

        private async Task UpdateStatus(string orderId , OrderStatus orderStatus ,  Guid userId , string currentName)
        {
            UpdateStatusOrderDto input = new UpdateStatusOrderDto()
            {
                OrderId = orderId,
                OrderStatus = orderStatus
            };
            var result = await orderService.UpdateStatusOrder(input);
            var notiUser = orderStatus == OrderStatus.Confirmed ? $"Đơn hàng của bạn đã được shipper {currentName} tiếp nhận" : $"Đơn hàng của bạn đang vận chuyển";
            var notiAdmin = orderStatus == OrderStatus.Confirmed ? $"Đơn hàng đã được shipper {currentName} tiếp nhận" : $"shipper {currentName} đã từ chối nhận đơn hàng";

            if (result)
            {
                NotiSendConfig notiSend = new NotiSendConfig("Cập nhật trạng thái đơn hàng", $"", $"/order/{orderId}");
                notiSend.Message = notiAdmin;
                await broadCastService.SendNotiAllAdmin(notiSend);
                notiSend.Message = notiUser;
                await broadCastService.SendNotiUserId(notiSend, userId);
                toastService.ShowSuccess("Cập nhật thành công");
            }
            else
            {                toastService.ShowSuccess("Cập nhật thất bại");

            }
            await LoadOrder();
            StateHasChanged();
        }
    }
}
