using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Entities.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.OrderService
{
    public interface IOrderService 
    {
        Task<List<Order>> GetAllOrders();
        Task<List<OrderDetails>> GetOrderDetails(string orderId);
        Task<Order> GetOrderWithUser(string orderId);
        Task<List<Order>> SearchOrder(string searchText);
        Task DeleteOrder(string orderId);
        Task<bool> AssignShipper(AssignShipperDto model);
        Task<List<SelectItem>> GetShippers();
        Task<List<Order>> GetOrderByShipper(OrderShipperSearchDto input);
        Task<List<Order>> GetOrderHistoryShipper();
        Task<bool> UpdateStatusOrder(UpdateStatusOrderDto input);
    }
}
