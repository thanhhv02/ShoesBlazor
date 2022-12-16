using EmailValidation;
using MailKit.Search;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities;
using PoPoy.Api.Data;
using PoPoy.Api.Helpers.TokenHelpers;
using PoPoy.Api.SendMailService;
using PoPoy.Api.VNPay;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.ApiModels;
using PoPoy.Shared.Dto.RefreshToken;
using PoPoy.Shared.Entities.Area;
using PoPoy.Shared.Enum;
using PoPoy.Shared.PayPal;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static PoPoy.Shared.PayPal.PayPalPaymentExecutedResponse;
using static System.Net.Mime.MediaTypeNames;

namespace PoPoy.Api.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ITokenService _tokenService;

        public AuthService(IConfiguration configuration,
                               UserManager<User> userManager,
                               SignInManager<User> signInManager,
                               IEmailService emailService,
                               RoleManager<Role> roleManager,
                               DataContext context,
                               IHttpContextAccessor httpContext,
                               IWebHostEnvironment env,
                               ITokenService tokenService)
        {
            this._emailService = emailService;
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _env = env;
            this._tokenService = tokenService;
            _httpContext = httpContext;
            _context = context;
        }
        public HttpContext Context => _httpContext.HttpContext;
        public Guid UserId() => Guid.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
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
                return new ServiceErrorResponse<string>(changePasswordResult.Errors.Select(x => x.Description).FirstOrDefault().ToString());
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
            user.UserName = null;
            return new ServiceSuccessResponse<User>(user);
        }

        public async Task<List<UserVM>> GetUserPaging()
        {
            var query = _userManager.Users;
            var data = await query.Select(user => new UserVM()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Dob = user.Dob,
                Id = user.Id,
                UserName = user.UserName,
                //Password = user.PasswordHash,
                LastName = user.LastName,
            }).ToListAsync();
            return data;
        }

        public async Task<LoginResponse<AuthResponseDto>> Login(LoginRequest login)
        {

            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null) return new LoginResponse<AuthResponseDto>() { Message = "Tài khoản không tồn tại", Success = false };

            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (!result.Succeeded) return new LoginResponse<AuthResponseDto>() { Message = "Tài khoản hoặc mật khẩu không đúng", Success = false };
            if (result.IsNotAllowed) return new LoginResponse<AuthResponseDto>() { Message = "Tài khoản không được cấp quyền vào trang này", Success = false };
            var roles = await _userManager.GetRolesAsync(user);


            var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(user);
            var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);
            var response = new AuthResponseDto()
            {
                Token = token,
                RefreshToken = refreshToken
            };

            return new LoginResponse<AuthResponseDto>() { Success = true, Data = response, RoleNames = roles.ToList() };
        }
        public async Task<ServiceResponse<bool>> CreateUser(CreateUser request)
        {
            var user = new User();
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.UserName;
            user.CreatedDate = DateTime.UtcNow;
            var result = await _userManager.CreateAsync(user, request.Password);
            foreach (var roleName in request.Roles)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            if (result.Succeeded)
            {
                return new ServiceSuccessResponse<bool>();

            }
            string error = "";
            result.Errors.ToList().ForEach(p => error += p.Description + ", ");
            return new ServiceErrorResponse<bool>(error);

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

            if (await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == user.PhoneNumber) != null)
                return new ServiceErrorResponse<bool>("Số điện thoại đã được đăng ký");

            if(EmailValidator.Validate(request.Email))
                return new ServiceErrorResponse<bool>("Cần nhập đúng định dạng email");

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

            string url = $"{_configuration["ApiUrl"]}/api/user/confirmemail?userid={user.Id}&token={validEmailToken}";

            EmailDto emailDto = new EmailDto
            {
                Subject = "Xác thực email người dùng",
                Body = $"<h1>Xin chào, {user.LastName + " " + user.FirstName}</h1><br/>"
                + $"<h3>Tài khoản: {user.UserName}</h3></br>"
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
            if (EmailValidator.Validate(model.Email))
                return new ServiceErrorResponse<string>("Cần nhập đúng định dạng email");

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
                    return new ServiceSuccessResponse<string>("Password has been reset successfully!");
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
                //Password = user.PasswordHash,
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

        public async Task<string> CheckOut(List<Cart> cartItem)
        {
            string OrderId = GenerateOrderId();
            var prods = _context.ProductQuantities.ToList();

            try
            {
                var detail = cartItem.FirstOrDefault();
                var address = await _context.Addresses.Where(x => x.UserId == detail.UserId).Select(x => x.Id).FirstOrDefaultAsync();
                Order order = new Order();
                order.Id = OrderId;
                order.UserId = detail.UserId;
                order.TotalPrice = detail.TotalPrice;
                order.OrderDate = DateTime.Now;
                order.PaymentMode = detail.PaymentMode;
                order.OrderStatus = OrderStatus.Processing;
                order.AddressId = address;
                _context.Orders.Add(order);
                var user = await _userManager.FindByIdAsync(detail.UserId.ToString());
                var userAddress = _context.Addresses.Where(x => x.UserId == detail.UserId).FirstOrDefault();
                List<string> products = new List<string>();
                foreach (var items in cartItem)
                {
                    string OrderId2 = GenerateOrderId();
                    OrderDetails _orderDetail = new OrderDetails();
                    _orderDetail.OrderIdFromOrder = OrderId;
                    _orderDetail.OrderId = OrderId2;
                    _orderDetail.ProductId = items.Product.Id;
                    _orderDetail.SizeName = items.SizeName; /*cartItem.Where(x => x.Product.Id == items.Product.Id).FirstOrDefault().Size;*/
                    _orderDetail.Price = items.Price;
                    _orderDetail.Quantity = items.Quantity;
                    _orderDetail.ColorName = items.ColorName;
                    _orderDetail.TotalPrice = (double)(items.Price * items.Quantity);
                    _context.OrderDetails.Add(_orderDetail);
                    products.Add("<tr>\r\n" +
                        "<td align=\"left\" style=\"padding:3px 9px\" valign=\"top\">\r\n" +
                        $"<span>{_context.Products.Where(x => x.Id == items.Product.Id).FirstOrDefault().Title} - {items.SizeName} - {items.ColorName}</span>\r\n" +
                        "<br>\r\n" +
                        "</td>\r\n" +
                        "<td align=\"left\" style=\"padding:3px 9px\" valign=\"top\">\r\n" +
                        $"<span>{items.Price}đ</span>\r\n" +
                        "</td>\r\n                                            " +
                        $"<td align=\"left\" style=\"padding:3px 9px\" valign=\"top\">{items.Quantity}</td>\r\n" +
                        "<td align=\"left\" style=\"padding:3px 9px\" valign=\"top\">\r\n" +
                        "<span>0đ</span>\r\n" +
                        "</td>\r\n" +
                        "<td align=\"right\" style=\"padding:3px 9px\" valign=\"top\">\r\n" +
                        $"<span>{(double)(items.Price * items.Quantity)}đ</span>\r\n" +
                        "</td>\r\n" +
                        "</tr>");
                    var selected_product = prods.Where(x => x.ProductId == items.ProductId && x.SizeId == items.SizeId).FirstOrDefault();
                    selected_product.Quantity = selected_product.Quantity - items.Quantity;
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == items.ProductId);
                    product.CheckoutCount++;
                    _context.ProductQuantities.Update(selected_product);
                    await _context.SaveChangesAsync();
                }

                var result = _context.SaveChanges();
                if (result != 1)
                {
                    //await SendMailOrderSuccessfully(OrderId, order, user, products, detail);
                    Thread thr = new Thread(delegate ()
                    {
                        var path = Path.Combine(_env.ContentRootPath, "wwwroot/ordersuccesfullymail.html");
                        string bodyBuilder = null;
                        using (StreamReader SourceReader = System.IO.File.OpenText(path))
                        {
                            bodyBuilder = SourceReader.ReadToEnd();
                        }

                        bodyBuilder = bodyBuilder.Replace("[oder-id]", OrderId.ToString().ToUpper());
                        bodyBuilder = bodyBuilder.Replace("[user-first-name]", user.FirstName);
                        bodyBuilder = bodyBuilder.Replace("[order-date]", order.OrderDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
                        bodyBuilder = bodyBuilder.Replace("[user-full-name]", user.LastName + " " + user.FirstName);
                        bodyBuilder = bodyBuilder.Replace("[user-mail]", user.Email);
                        bodyBuilder = bodyBuilder.Replace("[user-list-order]", _configuration["ClientUrl"] + "/user/list-order");
                        bodyBuilder = bodyBuilder.Replace("[user-phone-number]", user.PhoneNumber);
                        bodyBuilder = bodyBuilder.Replace("[payment-mode]", order.PaymentMode);
                        bodyBuilder = bodyBuilder.Replace("[total-price]", order.TotalPrice.ToString());
                        bodyBuilder = bodyBuilder.Replace("[all-products]", String.Join("", products.ToArray()));
                        bodyBuilder = bodyBuilder.Replace("[user-address]", userAddress.Street+" "+userAddress.Ward+" "+userAddress.District+" "+userAddress.City);
                        EmailDto emailDto = new EmailDto
                        {
                            Subject = $"[Popoy] Xác nhận đơn hàng #{OrderId.ToString().ToUpper()}",
                            Body = bodyBuilder,
                            To = user.Email
                        };
                        try
                        {
                            _emailService.SendEmail(emailDto);
                        }
                        catch
                        {

                        }
                    })
                    {
                        IsBackground = true
                    };
                    thr.Start();
                    return OrderId;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateOrderId()
        {
            string OrderId = string.Empty;
            Random generator = null;
            for (int i = 0; i < 1000; i++)
            {
                generator = new Random();
                OrderId = "p" + generator.Next(1, 10000000).ToString("D8");
                if (!_context.Orders.Where(x => x.Id == OrderId).Any())
                {
                    break;
                }
            }
            return OrderId;
        }
        public async Task<bool> UpdatePaymentStatus(Guid userId)
        {
            try
            {
                var orders = _context.Orders.Where(x => x.UserId == userId && x.PaymentMode == "PayPal").Select(o => o);
                foreach (var order in orders)
                {
                    order.PaymentStatus = PaymentStatus.Paid;
                    _context.Orders.Update(order);
                }

                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> MakePaymentPaypal(double total)
        {
            try
            {
                var payPalAPI = new PayPalAPI(_configuration);
                string url = await payPalAPI.getRedirectURLToPayPal(total, "USD");
                return url;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<bool> CreateAddress(Address address)
        {
            Address add = new Address()
            {
                City = address.City,
                District = address.District,
                Ward = address.Ward,
                Street = address.Street,
                UserId = address.UserId,
            };
            await _context.Addresses.AddAsync(add);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public Guid GetUserId() => Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<Address>> GetAddress(Guid userId)
        {
            //var userId = GetUserId();
            var address = await _context.Addresses
                .FirstOrDefaultAsync(x => x.UserId == userId);
            return new ServiceResponse<Address> { Data = address };
        }
        public async Task<List<SelectItem>> GetShippers()
        {
            var list = await _userManager.GetUsersInRoleAsync(roleName: RoleName.Shipper);
            return list.Select(p => new SelectItem { Id = p.Id.ToString(), Name = p.FirstName + " " + p.LastName }).ToList();

        }
        public async Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address, Guid userId)
        {
            var response = new ServiceResponse<Address>();
            var dbAddress = (await GetAddress(userId)).Data;
            if (dbAddress == null)
            {
                address.UserId = userId;
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();
                response.Data = address;
            }
            else
            {
                dbAddress.Ward = address.Ward;
                dbAddress.District = address.District;
                dbAddress.City = address.City;
                dbAddress.Street = address.Street;
                response.Data = dbAddress;
            }

            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<List<Address>> GetListAddress()
        {
            var query = from a in _context.Addresses
                        select new { a };

            //var user = await (from a in _context.Addresses
            //                          join u in _context.Users on a.UserId equals u.Id
            //                          select a).ToListAsync();

            return await query.Select(x => new Address()
            {
                Id = x.a.Id,
                UserId = x.a.UserId,
                Street = x.a.Street,
                City = x.a.City,
                Ward = x.a.Ward,
                District = x.a.District
            }).ToListAsync();
        }

        public async Task<ServiceResponse<List<UploadResult>>> UploadUserImage(List<IFormFile> files, string userId)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                //string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                //var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);

                //trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(_env.ContentRootPath, "wwwroot/uploads", untrustedFileName);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);

                uploadResult.StoredFileName = untrustedFileName;
                uploadResult.ContentType = file.ContentType;
                uploadResults.Add(uploadResult);

                var currentAvatar = user.AvatarPath != null ? user.AvatarPath : null;

                if (currentAvatar != null)
                {
                    var GET_FILE_NAME_FROM_PATH = currentAvatar.Replace(_configuration["ApiUrl"] + "/uploads/", "");
                    var pathav = Path.Combine(_env.ContentRootPath, "wwwroot/uploads", GET_FILE_NAME_FROM_PATH);
                    if (System.IO.File.Exists(pathav))
                    {
                        System.IO.File.Delete(pathav);
                    }
                }


                user.AvatarPath = _configuration["ApiUrl"] + "/uploads/" + untrustedFileName;
            }

            await _context.SaveChangesAsync();

            return new ServiceSuccessResponse<List<UploadResult>>(uploadResults);
        }

        public string MakePaymentVNPay(double total)
        {
            string url = _configuration["VnPay:Url"];
            string returnUrl = _configuration["VnPay:ReturnUrl"];
            string tmnCode = _configuration["VnPay:TmnCode"];
            string hashSecret = _configuration["VnPay:HashSecret"];

            PayLib pay = new PayLib();
            Util util = new Util(_httpContext);

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", total.ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return paymentUrl;
        }

        private async void SendMailOrderSuccessfully(string OrderId, Order order, User user, List<string> products, Cart detail)
        {
            var path = Path.Combine(_env.ContentRootPath, "wwwroot/ordersuccesfullymail.html");
            string bodyBuilder = null;
            using (StreamReader SourceReader = System.IO.File.OpenText(path))
            {
                bodyBuilder = SourceReader.ReadToEnd();
            }

            bodyBuilder = bodyBuilder.Replace("[oder-id]", OrderId.ToString().ToUpper());
            bodyBuilder = bodyBuilder.Replace("[user-first-name]", user.FirstName);
            bodyBuilder = bodyBuilder.Replace("[order-date]", order.OrderDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            bodyBuilder = bodyBuilder.Replace("[user-full-name]", user.LastName + " " + user.FirstName);
            bodyBuilder = bodyBuilder.Replace("[user-mail]", user.Email);
            bodyBuilder = bodyBuilder.Replace("[user-list-order]", _configuration["ClientUrl"] + "/user/list-order");
            bodyBuilder = bodyBuilder.Replace("[user-phone-number]", user.PhoneNumber);
            bodyBuilder = bodyBuilder.Replace("[payment-mode]", order.PaymentMode);
            bodyBuilder = bodyBuilder.Replace("[total-price]", order.TotalPrice.ToString());
            bodyBuilder = bodyBuilder.Replace("[all-products]", String.Join("", products.ToArray()));
            bodyBuilder = bodyBuilder.Replace("[user-address]", await _context.Addresses.Where(x => x.UserId == detail.UserId)
                .Select(x => x.Street + " " + x.Ward + " " + x.District + " " + x.City).FirstOrDefaultAsync());
            EmailDto emailDto = new EmailDto
            {
                Subject = $"[Popoy] Xác nhận đơn hàng #{OrderId.ToString().ToUpper()}",
                Body = bodyBuilder,
                To = user.Email
            };
            try
            {
                _emailService.SendEmail(emailDto);
            }
            catch
            {

            }
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var newUser = await _userManager.GetUserAsync(_httpContext.HttpContext.User);
            return newUser;
        }

        public async Task<ServiceResponse<bool>> UpdateUserProfile(User user)
        {
            var findUser = await _userManager.FindByIdAsync(user.Id.ToString());
            if(findUser != null)
            {
                var isExistPhoneNumber = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == user.PhoneNumber) != null 
                    && findUser.PhoneNumber != user.PhoneNumber;

                if (isExistPhoneNumber)
                    return new ServiceErrorResponse<bool>("Số điện thoại đã được đăng ký");

                var isExistEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email) != null
                    && findUser.Email  != user.Email;

                if (isExistEmail)
                    return new ServiceErrorResponse<bool>("Email này đã được đăng ký");
                try
                {
                    findUser.LastName = user.LastName;
                    findUser.FirstName = user.FirstName;
                    findUser.Email = user.Email;
                    findUser.Dob = user.Dob;
                    findUser.PhoneNumber = user.PhoneNumber;

                    var result = await _userManager.UpdateAsync(findUser);

                    if (result.Succeeded)
                    {
                        return new ServiceSuccessResponse<bool>();
                    }
                }
                catch
                {

                }
                
            }
            
            return new ServiceErrorResponse<bool>("Cập nhật không thành công");
        }

        public async Task<bool> DeleteUserAvatar(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                return false;
            }

            user.AvatarPath = null;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return false;

            return true;
        }
    }
}
