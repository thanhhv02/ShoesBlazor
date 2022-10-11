using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoPoy.Api.Services.OrderService;
using PoPoy.Shared.Dto;
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
        private string GetUserId()
        => _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
