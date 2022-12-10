using PoPoy.Api.Data;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Entities;
using PoPoy.Shared.Entities.OrderDto;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<List<OrderDetails>> GetOrderDetails(string OrderId);
        Task<Order> GetOrderWithUser(string orderId);
        Task<List<Order>> SearchOrder(string searchText);
        Task<int> DeleteOrder(string orderId);
        Task<PagedList<OrderOverviewResponse>> GetOrders(ProductParameters productParameters, Guid userId);
        Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetailsForClient(string orderId);
        Task<bool> AssignShipper(AssignShipperDto model);
        Task<List<Order>> GetOrderByShipper(OrderShipperSearchDto input);
        Task<List<Order>> OrderHistoryShipper(Guid userid);
        Task<bool> UpdateStatusOrder(UpdateStatusOrderDto input);
        Task<Order> FindOrderById(string id);
        Task<bool> HasOrderById(string id);
        Task<Refund> CancelOrder(Order order);
    }
}
