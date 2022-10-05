using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoPoy.Api.Migrations
{
    public partial class SeedStockToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bf52fa4a-0513-4c04-acb6-dc77f100a156"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("0dfd2f4b-8db8-4bf9-9797-c74c8bee3670"), 0, null, "fc5c0343-df14-41c0-bab8-f3964737cdf2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hovanthanh12102002@gmail.com", true, "Van Thanh", "Ho", false, null, "hovanthanh12102002@gmail.com", "thanhhv", "AQAAAAEAACcQAAAAEIfPJ+WSaz/rS/+jGf4yumb5pDavBMvWq+flnEp4ZpmXy9Z+i2rxhsEIeiOGSlItsQ==", "032132131", false, "9ddd5ea9-a50e-46d2-bd41-8ab423b0eff9", false, "thanhhv" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 534, DateTimeKind.Local).AddTicks(3095), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3573), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3606), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3609), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3611), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3614), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3616), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3619), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3621), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3624), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3626), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3629), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3631), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3634), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3636), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3638), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3641), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 5, 10, 45, 37, 535, DateTimeKind.Local).AddTicks(3643), 1000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("0dfd2f4b-8db8-4bf9-9797-c74c8bee3670"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("bf52fa4a-0513-4c04-acb6-dc77f100a156"), 0, null, "d4170e99-bd54-41ee-b9a7-9552636e90e9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hovanthanh12102002@gmail.com", true, "Van Thanh", "Ho", false, null, "hovanthanh12102002@gmail.com", "thanhhv", "AQAAAAEAACcQAAAAEIfPJ+WSaz/rS/+jGf4yumb5pDavBMvWq+flnEp4ZpmXy9Z+i2rxhsEIeiOGSlItsQ==", "032132131", false, "5b38041e-b886-4252-85ea-617e0ba45400", false, "thanhhv" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 207, DateTimeKind.Local).AddTicks(6164), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5203), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5248), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5253), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5256), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5259), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5263), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5267), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5271), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5274), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5278), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5281), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5285), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5288), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5292), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5297), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5300), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5304), 0 });
        }
    }
}
