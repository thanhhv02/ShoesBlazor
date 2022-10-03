using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoPoy.Api.Migrations
{
    public partial class FixTableProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("0c8ec976-caa6-4cad-89e1-e15dce130523"));

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "CheckoutCount");

            migrationBuilder.AddColumn<int>(
                name: "CheckoutCount",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("bf52fa4a-0513-4c04-acb6-dc77f100a156"), 0, null, "d4170e99-bd54-41ee-b9a7-9552636e90e9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hovanthanh12102002@gmail.com", true, "Van Thanh", "Ho", false, null, "hovanthanh12102002@gmail.com", "thanhhv", "AQAAAAEAACcQAAAAEIfPJ+WSaz/rS/+jGf4yumb5pDavBMvWq+flnEp4ZpmXy9Z+i2rxhsEIeiOGSlItsQ==", "032132131", false, "5b38041e-b886-4252-85ea-617e0ba45400", false, "thanhhv" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 207, DateTimeKind.Local).AddTicks(6164) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5203) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5248) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5253) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5256) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5259) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5263) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5267) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5271) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5274) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5278) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5281) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5285) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5288) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5292) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5297) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CheckoutCount", "DateCreated" },
                values: new object[] { 0, new DateTime(2022, 10, 3, 8, 8, 30, 209, DateTimeKind.Local).AddTicks(5304) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bf52fa4a-0513-4c04-acb6-dc77f100a156"));

            migrationBuilder.DropColumn(
                name: "CheckoutCount",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "CheckoutCount",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("0c8ec976-caa6-4cad-89e1-e15dce130523"), 0, null, "0aeaf784-6779-4a72-80e2-f9a5e29196c1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hovanthanh12102002@gmail.com", true, "Van Thanh", "Ho", false, null, "hovanthanh12102002@gmail.com", "thanhhv", "AQAAAAEAACcQAAAAEIfPJ+WSaz/rS/+jGf4yumb5pDavBMvWq+flnEp4ZpmXy9Z+i2rxhsEIeiOGSlItsQ==", "032132131", false, "3cc636bd-e4e6-4ee9-a10f-5481f0e37199", false, "thanhhv" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 183, DateTimeKind.Local).AddTicks(6320), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7810), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7834), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7837), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7840), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7842), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7845), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7849), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7852), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7855), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7857), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7860), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7863), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7865), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7868), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7871), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7874), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "Quantity" },
                values: new object[] { new DateTime(2022, 10, 1, 7, 22, 55, 184, DateTimeKind.Local).AddTicks(7877), 1000 });
        }
    }
}
