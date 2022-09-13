using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Data
{
    public class DbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async Task SeedAsync(DataContext context, ILogger<DbContextSeed> logger)
        {
            var temp = context.Users.Any().ToString();
            if (!context.Users.Any() || temp.Contains("Admin"))
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mr",
                    LastName = "A",
                    Email = "admin1@gmail.com",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    PhoneNumber = "032132131",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123$");
                var role = new IdentityUserRole<Guid>()
                {
                    UserId = user.Id,
                    RoleId = Guid.Parse("17fcf0db-52ee-49af-be5f-ab38c222ee38")
                };
                context.Users.Add(user);
                context.UserRoles.Add(role);
            }

            await context.SaveChangesAsync();
        }
    }
}
