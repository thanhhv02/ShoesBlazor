using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PoPoy.Api.Data
{
    public class DbContextSeed
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public DbContextSeed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedAsync(ILogger<DbContextSeed> logger)
        {
            var rolenames = typeof(RoleName).GetFields();
            foreach (var item in rolenames)
            {
                string name = item.GetRawConstantValue().ToString();
                var ffound = await roleManager.FindByNameAsync(name);
                if (ffound == null)
                {
                    await roleManager.CreateAsync(new Role(name));
                }
            }
            //tạo user admin : admin - Admin@123$ 
            var useradmin = await userManager.FindByEmailAsync("admin1@gmail.com");

            if (useradmin == null)
            {
                useradmin = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mr",
                    LastName = "A",
                    Email = "admin1@gmail.com",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    PhoneNumber = "032132131",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    EmailConfirmed = true // không cần xác thực email nữa , 
                };
                await userManager.CreateAsync(useradmin, "Admin@123$");
                await userManager.AddToRoleAsync(useradmin, RoleName.Admin);
            }


            // tạo user khách hàng
            //tạo user admin : thanh - thanh123 
            var user = await userManager.FindByEmailAsync("thanhitc@gmail.com");
            if (user == null)
            {
                user = new User()
                {
                    Id = Guid.NewGuid(),

                    FirstName = "Mr",
                    LastName = "Thành đẹp trai",
                    Email = "thanhitc@gmail.com",
                    NormalizedEmail = "THANHITC@GMAIL.COM",
                    PhoneNumber = "032232131",
                    UserName = "thanhitc",
                    NormalizedUserName = "THANHITC",
                    EmailConfirmed = true // không cần xác thực email nữa , 
                };
                await userManager.CreateAsync(user, "thanh123");
                await userManager.AddToRoleAsync(user, RoleName.Customer);
            }


        }

    }
}
