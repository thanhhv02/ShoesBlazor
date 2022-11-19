using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.AuthService;
using PoPoy.Api.Services.OrderService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Entities;
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
        private readonly IAuthService _auth;
        public OrderController(IOrderService orderService, IHttpContextAccessor httpContext, IAuthService authService)
        {
            _orderService = orderService;
            _httpContext = httpContext;
            this._auth = authService;
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
        [AuthorizeToken()]
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
        [AuthorizeToken]
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
        [AuthorizeToken(AuthorizeToken.ADMIN_STAFF)]
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
        [AuthorizeToken(AuthorizeToken.ADMIN_SHIPPER)]

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
        [AuthorizeToken(AuthorizeToken.PAGEADMIN)]
        public async Task<ActionResult<bool>> UpdateStatusOrder(UpdateStatusOrderDto input)
        {
            var result = await _orderService.UpdateStatusOrder(input);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("cancel-order")]
        [AuthorizeToken]
        public async Task<ActionResult<bool>> CancelOrder(string id)
        {
            if (!await _orderService.HasOrderById(id))
            {
                return BadRequest(new ServiceErrorResponse<string>("no order"));
            }
            var order = await _orderService.FindOrderById(id);

            var currentUser = await _auth.GetCurrentUserAsync();

            if (!currentUser.Id.Equals(order.UserId))
            {
                return BadRequest(new ServiceErrorResponse<string>("something went wrong!"));
            }

            var result = await _orderService.CancelOrder(order);
            result.Order = null;

            return Ok(new ServiceSuccessResponse<Refund>()
            {
                Success = true,
                Data = new Refund()
                {
                    DateRefunded = result.DateRefunded,
                    RefundRate = result.RefundRate
                }
            });
        }


        private string GetUserId()
        => _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
