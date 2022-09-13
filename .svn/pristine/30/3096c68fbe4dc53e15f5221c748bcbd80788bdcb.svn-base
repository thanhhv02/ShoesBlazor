using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PoPoy.Api.Services.AuthService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.ViewModels;
using System;
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
        public async Task<IActionResult> GetUserId([FromBody] LoginRequest request)
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
        public async Task<ServiceResponse<User>> GetUserFromId (string id)
        {
            if (id == null)
                return new ServiceResponse<User>();

            var result = await _authService.GetUserFromId(id);

            return result;
        }

    }
}
