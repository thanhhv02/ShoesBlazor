using PoPoy.Shared.Dto;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(LoginRequest loginRequest);
        Task<ServiceResponse<string>> ForgotPassword(string email);
        Task<ServiceResponse<bool>> Register(RegisterRequest registerRequest);
        Task<ServiceResponse<User>> GetUserFromId(string id);
        Task<ServiceResponse<string>> ChangePassword(ChangePasswordRequest changePasswordRequest);
        Task<ServiceResponse<string>> ResetPassword(ResetPasswordRequest resetPasswordRequest);
        Task<string> Checkout(List<Cart> cartItems);
        Task<bool> CreateAddress(Address address);
        Task<Address> GetAddress(Guid userId);
        Task<Address> AddOrUpdateAddress(Address address, Guid userId);
        Task<bool> IsUserAuthenticated();
        Task<Guid> GetUserId(LoginRequest request);
        Task<string> MakePayPalPayment(double total);
        Task<string> MakeVNPayPayment(double total);
        Task Logout();
    }
}
