using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoPoy.Api.Helpers.TokenHelpers;
using PoPoy.Shared.Dto.RefreshToken;
using PoPoy.Shared.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System;
using PoPoy.Shared.ViewModels;

namespace PoPoy.Api.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public TokenController(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto tokenDto)
        {
            try
            {
                if (tokenDto is null)
                {
                    return BadRequest(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = "Invalid client request" });
                }
                var principal = _tokenService.GetPrincipalFromExpiredToken(tokenDto.Token);
                var username = principal.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                    return BadRequest(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = "Invalid client request 2" });
                var signingCredentials = _tokenService.GetSigningCredentials();
                var claims = await _tokenService.GetClaims(user);
                var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                user.RefreshToken = _tokenService.GenerateRefreshToken();
                await _userManager.UpdateAsync(user);
                return Ok(new AuthResponseDto { Token = token, RefreshToken = user.RefreshToken, IsAuthSuccessful = true });
            }
            catch (Exception e)
            {

                return BadRequest(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = e.Message });

            }
        }
    }
}
