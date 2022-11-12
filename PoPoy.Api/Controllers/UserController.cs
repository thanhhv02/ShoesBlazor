using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.AuthService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using PoPoy.Shared.PayPal;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public UserController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginRequest request)
        {
            var response = await _authService.Login(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("getUser")]
        public async Task<IActionResult> GetUserPaging()
        {
            var users = await _authService.GetUserPaging();
            return Ok(users);
        }

        [HttpGet("getRoles")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _authService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _authService.GetById(id);
            return Ok(user);
        }

        [HttpGet("getListAddress")]
        public async Task<ActionResult<List<Address>>> GetListAddress()
        {
            var addresses = await _authService.GetListAddress();
            return Ok(addresses);
        }

        [HttpGet("searchUser/{searchText}")]
        public async Task<ActionResult<ServiceResponse<UserSearchResult>>> SearchProducts(string searchText)
        {
            var result = await _authService.SearchUser(searchText);
            if (result.Count == 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(RegisterRequest request)
        {
            var response = await _authService.Register(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("getUserById/{id}")]

        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _authService.GetUserById(id);
            return Ok(user);
        }

        [HttpPut("userUpdate/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.UpdateUser(id, request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [AuthorizeToken(RoleName.Admin)]
            
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _authService.DeleteUser(id);
            return Ok(user);
        }

        [HttpGet]
        [HttpPost("forgot-password")]
        public async Task<ActionResult<ServiceResponse<string>>> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound(new ServiceErrorResponse<string>("Chưa nhập email"));

            var result = await _authService.ForgetPassword(email);

            if (result.Success)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _authService.ConfirmEmail(userId, token);

            if (result.Success)
            {
                return Redirect($"{_configuration["ApiUrl"]}/ConfirmEmail.html");
            }

            return BadRequest(result);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasword(ChangePasswordRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.ChangePassword(model);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.ResetPassword(model);

                if (result.Success)
                    return Redirect($"{_configuration["ApiUrl"]}/resetpassword.html"); ;

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPost("getUserId")]
        public async Task<IActionResult> GetUserId(LoginRequest request)
        {
            var result = await _authService.GetUserId(request);
            return Ok(result);
        }

        [HttpPut("roles/{id}")]
        public async Task<IActionResult> RoleAssgign(Guid id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RoleAssign(id, request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("get-user-from-id")]
        //[Authorize]
        public async Task<ServiceResponse<User>> GetUserFromId(string id)
        {
            if (id == null)
                return new ServiceResponse<User>();

            var result = await _authService.GetUserFromId(id);

            return result;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutAsync(List<Cart> cartItems)
        {
            var record = cartItems.FirstOrDefault();
            if (record != null)
            {
                if (record.PaymentMode == "CashOnDelivery")
                {
                        var result = await _authService.CheckOut(cartItems);
                        return Ok(result);
                }
                if (record.PaymentMode == "PayPal")
                {
                    var data = _authService.MakePaymentPaypal(record.PayPalPayment);
                    if (data != null)
                    {
                        var ref_number = data.Result.Split("&")[1];
                        cartItems.FirstOrDefault().orderReference = ref_number.Split("=")[1];
                        var result = await _authService.CheckOut(cartItems);
                     
                            return Ok(result);
                    }
                }
                if (record.PaymentMode == "VNPay")
                {
                    var data = _authService.MakePaymentVNPay(record.PayPalPayment);
                    if (data != null)
                    {
                        var result = await _authService.CheckOut(cartItems);
                        return Ok(result);
                    }
                }
            }
            return BadRequest(record);
        }

        [HttpGet("paymentPaypal")]
        public async Task<IActionResult> PaymentPaypal(string paymentId, string payerId, Guid userId)
        {
            try
            {
                var payPalAPI = new PayPalAPI(_configuration);
                var status = await payPalAPI.executedPayment(paymentId, payerId);
                if (status.state == "approved")
                {
                    var result = await _authService.UpdatePaymentStatus(userId);
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("checkoutPayPal")]
        public async Task<IActionResult> CheckoutPayPal(double total)
        {
            var url = await _authService.MakePaymentPaypal(total);
            return Ok(url);
        }

        [HttpGet("checkoutVNPay")]
        public IActionResult CheckoutVNPay(double total)
        {
            var url = _authService.MakePaymentVNPay(total);
            return Ok(url);
        }

        [HttpPost("address")]
        public IActionResult CreateAddress(Address address)
        {
            var data = _authService.CreateAddress(address);
            return Ok(data);
        }

        [HttpGet("getAddress")]
        public async Task<ActionResult<ServiceResponse<Address>>> GetAddress(Guid userId)
        {
            return await _authService.GetAddress(userId);
        }
        [HttpPost("addOrUpdateAddress")]
        public async Task<ActionResult<ServiceResponse<Address>>> AddOrUpdateAddress(Address address, Guid userId)
        {
            return await _authService.AddOrUpdateAddress(address, userId);
        }
        [HttpPost("upload-image")]
        public async Task<ActionResult<List<UploadResult>>> UploadFile(List<IFormFile> files, string id)
        {
            var uploadResults = await _authService.UploadUserImage(files, id);

            return Ok(uploadResults.Data);
        }

        [HttpGet("GetShippers")]
        public async Task<ActionResult<ServiceResponse<Address>>> GetShippers()
        {
            return Ok(await _authService.GetShippers());
        }
    }
}
