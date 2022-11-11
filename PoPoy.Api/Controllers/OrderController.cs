using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoPoy.Api.Services.OrderService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Entities.OrderDto;
using PoPoy.Shared.Enum;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PoPoy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContext;
        public OrderController(IOrderService orderService, IHttpContextAccessor httpContext)
        {
            _orderService = orderService;
            _httpContext = httpContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("orderDetails/{orderId}")]
        public async Task<ActionResult<List<Order>>> GetOrderDetails(string orderId)
        {
            var orderDetails = await _orderService.GetOrderDetails(orderId);
            return Ok(orderDetails);
        }
        [HttpGet("getOrderWithUser/{orderId}")]
        public async Task<ActionResult<List<Order>>> GetOrderWithUser(string orderId)
        {
            var orderDetails = await _orderService.GetOrderWithUser(orderId);
            return Ok(orderDetails);
        }
        [HttpGet("searchOrder/{searchText}")]
        public async Task<IActionResult> SearchProduct(string searchText)
        {
            var result = await _orderService.SearchOrder(searchText);
            if (result.Count == 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteProduct(string orderId)
        {
            var affectedResult = await _orderService.DeleteOrder(orderId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok(affectedResult);
        }
        [HttpGet("get-all-order-user")]
        [Authorize]
        public async Task<ActionResult<List<OrderOverviewResponse>>> GetOrders([FromQuery] ProductParameters productParameters)
        {
            var result = await _orderService.GetOrders(productParameters, GetUserId());
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);
        }
        [HttpGet("get-order-detail-user/{orderId}")]
        public async Task<ActionResult<ServiceResponse<OrderDetailsResponse>>> GetOrderDetailsForClient(string orderId)
        {
            var result = await _orderService.GetOrderDetailsForClient(orderId);
            return Ok(result);
        }
        //[Authorize(Roles = RoleName.Admin)]
        [HttpPost("AssignShipper")]
        public async Task<ActionResult<ServiceResponse<bool>>> AssignShipper(AssignShipperDto model)
        {
            var result = await _orderService.AssignShipper(model);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("Không thêm được người vận chuyển");
        }
        [HttpPost("GetOrderByShipper")]
        //[Authorize(Roles = RoleName.Shipper + "," + RoleName.Admin)]
        public async Task<ActionResult<ServiceResponse<bool>>> GetOrderByShipper(OrderShipperSearchDto input)
        {
            var result = await _orderService.GetOrderByShipper(input);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("UpdateStatusOrder")]
        //[Authorize(Roles = RoleName.Shipper + "," + RoleName.Admin + RoleName.Staff )]


        public async Task<ActionResult<bool>> UpdateStatusOrder(UpdateStatusOrderDto input)
        {
            var result = await _orderService.UpdateStatusOrder(input);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
      

        private string GetUserId()
        => _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
