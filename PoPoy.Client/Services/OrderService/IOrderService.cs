using PoPoy.Shared.Dto;
using PoPoy.Shared.Entities;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.OrderService
{
    public interface IOrderService
    {
        event Action OrderListChanged;
        event Action OrderDetailsChanged;
        PagingResponse<OrderOverviewResponse> ListOrderResponse { get; set; }
        OrderDetailsResponse ListOrderDetailsResponse { get; set; }
        Task GetOrders(ProductParameters productParameters);
        Task GetOrderDetails(string orderId);
        Task DeleteOrder(string orderId);
        Task<Order> GetOrderWithUser(string orderId);
        Task<Refund> CancelOrder(string id);
        Task SavePaymentUrl(string orderId, string url);
        Task<string> GetPaymentUrl(string orderId);
        Task<DistanceDto> DrivingDistancebyAddress(string address);
    }
}
