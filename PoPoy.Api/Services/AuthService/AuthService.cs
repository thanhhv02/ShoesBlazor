﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PoPoy.Api.Data;
using PoPoy.Api.SendMailService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.ApiModels;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;
        public AuthService(IConfiguration configuration,
                               UserManager<User> userManager,
                               SignInManager<User> signInManager,
                               IEmailService emailService,
                               RoleManager<Role> roleManager,
                               DataContext context)
        {
            this._emailService = emailService;
            _configuration = configuration;
            _signInManager = signInManager;        
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<Guid> GetUserId(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var id = await _userManager.GetUserIdAsync(user);
            return Guid.Parse(id);
        }
        public async Task<ServiceResponse<string>> ChangePassword(ChangePasswordRequest model)
        {
            var userId = model.UserId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceErrorResponse<string>($"Unable to load user with ID '{userId}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                var error = new string[changePasswordResult.Errors.Count()];
                var listError =  new List<string>();
                foreach (var item in changePasswordResult.Errors)
                {
                    listError.Add(item.Description);
                }
                error = listError.ToArray();
                return new ServiceErrorResponse<string>(error);
            }
            else
            {
                await _signInManager.RefreshSignInAsync(user);
                return new ServiceSuccessResponse<string>("Your Password has been reset");
            }
        }

        public async Task<ServiceResponse<string>> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ServiceErrorResponse<string>($"Unable to load user with ID '{userId}'.");
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new ServiceSuccessResponse<string>("Email is confirmed!");

            return new ServiceErrorResponse<string>("Email did not confirm");
        }

        public async Task<ServiceResponse<string>> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new ServiceErrorResponse<string>("Tài khoản không tồn tại");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["ClientUrl"]}/ResetPassword?email={email}&token={validToken}";

            EmailDto emailDto = new EmailDto
            {
                Subject = "Đặt lại mật khẩu",
                Body = $"<h1>Làm theo hướng dẫn để đặt lại mật khẩu của bạn</h1>" +
                $"<p>Tên đăng nhập của bạn là: </p><h3>{user.UserName}</h3>" +
                $"<p>Để đặt lại mật khẩu <a href='{url}'>Bấm vào đây</a></p>",
                To = user.Email
            };

            try
            {
                _emailService.SendEmail(emailDto);
            }
            catch
            {
                return new ServiceErrorResponse<string>("Không thể gửi mail");
            }

            return new ServiceSuccessResponse<string>("Reset password URL has been sent to the email successfully!");
        }

        public async Task<ServiceResponse<User>> GetUserFromId(string idUser)
        {
            var user = await _userManager.FindByIdAsync(idUser);
            if (user == null) return new ServiceResponse<User>();

            return new ServiceSuccessResponse<User>(user);
        }


        public async Task<List<User>> GetUserPaging()
        {
            var query = _userManager.Users;
            var data = await query.Select(x => new User()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName
                }).ToListAsync();  
            return data;
        }

        public async Task<ServiceResponse<string>> Login(LoginRequest login)
        {

            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null) return new ServiceErrorResponse<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (!result.Succeeded) return new ServiceErrorResponse<string>("Tài khoản hoặc mật khẩu không đúng");
            if(result.IsNotAllowed) return new ServiceErrorResponse<string>("Tài khoản không được cấp quyền vào trang này");
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Email,user.Email ),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);       

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new ServiceSuccessResponse<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ServiceResponse<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null)
            {
                return new ServiceErrorResponse<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ServiceErrorResponse<bool>("Email đã tồn tại");
            }

            user = new User()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new ServiceErrorResponse<bool>("Đăng ký không thành công");
            }
            if (result.Succeeded)
            {
                var defaultrole = _roleManager.FindByNameAsync("Customer").Result;
                if (defaultrole != null)
                {
                    IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
                }
            }
            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = $"{ _configuration["ApiUrl"]}/api/user/confirmemail?userid={user.Id}&token={validEmailToken}";

            EmailDto emailDto = new EmailDto
            {
                Subject = "Xác thực email người dùng",
                Body = $"<h1>Xin chào, {user.LastName +" "+ user.FirstName}</h1><br/>"
                + $"<h3>Tài khoản: {user.UserName}</h3></br>" 
                + $"<h3>Mật khẩu: {request.Password}</h3></br>"
                + $"<p>Hãy xác nhận email của bạn <a href='{url}'>Bấm vào đây</a></p>",
                To = user.Email
            };
            try
            {
                _emailService.SendEmail(emailDto);
            }
            catch
            {
                return new ServiceErrorResponse<bool>("Không thể gửi mail");
            }
            
            return new ServiceSuccessResponse<bool>();
        }

        public async Task<ServiceResponse<string>> ResetPassword(ResetPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new ServiceErrorResponse<string>("No user associated with email");

            if (model.NewPassword != model.ConfirmPassword)
                return new ServiceErrorResponse<string>("Password doesn't match its confirmation");


            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            try
            {
                var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);

                if (result.Succeeded)
                    return new ServiceErrorResponse<string>("Password has been reset successfully!");
                else
                {
                    return new ServiceErrorResponse<string>(result.Errors.FirstOrDefault().Description.ToString());
                }
            }
            catch
            {
                return new ServiceErrorResponse<string>("Something went wrong !");
            }
            

            
        }

        public async Task<ServiceResponse<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ServiceErrorResponse<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ServiceSuccessResponse<bool>();
        }

        public async Task<List<User>> SearchUser(string searchText)
        {

            var users = await _context.Users
                    .Where(p => p.UserName.ToLower().Contains(searchText.ToLower()) ||
                           p.Email.ToLower().Contains(searchText.ToLower()))
                           .ToListAsync();

            return users;
        }

        public async Task<ServiceResponse<UserVM>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ServiceErrorResponse<UserVM>("Tài khoản không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userViewModel = new UserVM()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Dob = user.Dob,
                Id = user.Id,
                UserName = user.UserName,
                Password = user.PasswordHash,
                LastName = user.LastName,
                Roles = roles,

            };
            return new ServiceSuccessResponse<UserVM>(userViewModel);
        }

        public async Task<List<RoleVM>> GetAllRoles()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            return roles;
        }

        public async Task<UserVM> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userViewModel = new UserVM()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Dob = user.Dob,
                Id = user.Id,
                UserName = user.UserName,
                Password = user.PasswordHash,
                LastName = user.LastName,
                Roles = roles,
            };
            return userViewModel;
        }

        public async Task<ServiceResponse<bool>> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ServiceErrorResponse<bool>("Tài khoản không tồn tại");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return new ServiceSuccessResponse<bool>();

            return new ServiceErrorResponse<bool>("Xóa người dùng thất bại");
        }

        public async Task<ServiceResponse<bool>> UpdateUser(Guid id, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                return new ServiceErrorResponse<bool>("Email đã tồn tại");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Dob = request.Dob;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.UserName;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ServiceSuccessResponse<bool>();
            }
            return new ServiceErrorResponse<bool>("Cập nhật không thành công");

        }
    }
}
