using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.OrderService
{
    public interface IOrderService
    {
        event Action OrderDetailsChanged;
        PagingResponse<OrderOverviewResponse> ListOrderResponse { get; set; }
        OrderDetailsResponse ListOrderDetailsResponse { get; set; }
        Task GetOrders(ProductParameters productParameters, string userId);
        Task GetOrderDetails(string orderId);
    }
}
