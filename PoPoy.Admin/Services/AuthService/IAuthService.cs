using PoPoy.Shared.Dto;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(LoginRequest loginRequest);
        Task<List<User>> GetUsers();
        Task<List<User>> SearchUser(string searchText);
        Task AssignRole(RoleAssignRequest request);
        Task<RoleAssignRequest> GetRoleAssignRequest(Guid id);
        Task<bool> UpdateUser(UserVM user);
        Task<bool> DeleteUser(Guid id);
        Task<Guid> GetUserId(LoginRequest request);
        Task<UserVM> GetUserById(Guid id);
        Task Logout();
    }
}
