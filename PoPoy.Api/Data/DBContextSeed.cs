using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PoPoy.Api.Helpers;
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
                    CreatedDate = AppExtensions.GetDateTimeNow(),
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
                    CreatedDate = AppExtensions.GetDateTimeNow(),

                    EmailConfirmed = true // không cần xác thực email nữa , 
                };
                await userManager.CreateAsync(user, "thanh123");
                await userManager.AddToRoleAsync(user, RoleName.Customer);
            }

            var user2 = await userManager.FindByEmailAsync("hovanthanh12102002@gmail.com");
            if (user2 == null)
            {
                user2 = new User()
                {
                    Id = Guid.NewGuid(),

                    FirstName = "Văn Thành",
                    LastName = "Hồ",
                    Email = "hovanthanh12102002@gmail.com",
                    NormalizedEmail = "hovanthanh12102002@gmail.com",
                    PhoneNumber = "032232131",
                    UserName = "thanhhv",
                    NormalizedUserName = "THANHHV",
                    CreatedDate = AppExtensions.GetDateTimeNow(),

                    EmailConfirmed = true // không cần xác thực email nữa , 
                };
                await userManager.CreateAsync(user2, "123321");
                await userManager.AddToRoleAsync(user2, RoleName.Customer);
            }


            var shipper = await userManager.FindByEmailAsync("shipper@gmail.com");
            if (shipper == null)
            {
                shipper = new User()
                {
                    Id = Guid.NewGuid(),

                    FirstName = "Hoàng",
                    LastName = "ne",
                    Email = "shipper@gmail.com",
                    NormalizedEmail = "shipper@gmail.com",
                    PhoneNumber = "032232151",
                    UserName = "shipper",
                    NormalizedUserName = "Long",
                    CreatedDate = AppExtensions.GetDateTimeNow(),
                    EmailConfirmed = true // không cần xác thực email nữa , 
                };
                await userManager.CreateAsync(shipper, "thanh123");
                await userManager.AddToRoleAsync(shipper, RoleName.Shipper);
            }
        }

    }
}
