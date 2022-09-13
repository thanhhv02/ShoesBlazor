﻿using PoPoy.Shared.Dto;
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
        Task Logout();
    }
}
