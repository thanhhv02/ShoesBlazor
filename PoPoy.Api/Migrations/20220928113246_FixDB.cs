using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoPoy.Api.Migrations
{
    public partial class FixDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("dd984ddb-ed66-4f1d-883c-5a14d6f6e44b"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2cfa3083-c8dc-4178-899a-a1342c90082b"), 0, null, "9505aad8-f902-4ec4-8f0c-c35979d442ed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hovanthanh12102002@gmail.com", true, "Van Thanh", "Ho", false, null, "hovanthanh12102002@gmail.com", "thanhhv", "AQAAAAEAACcQAAAAEIfPJ+WSaz/rS/+jGf4yumb5pDavBMvWq+flnEp4ZpmXy9Z+i2rxhsEIeiOGSlItsQ==", "032132131", false, "05d947b3-afcf-4599-babc-cbed09a1fb42", false, "thanhhv" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 707, DateTimeKind.Local).AddTicks(6238));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5359));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5413));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5417));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5422));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5425));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5428));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5432));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5436));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5440));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5443));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5446));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5451));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5454));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5461));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5465));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 32, 44, 710, DateTimeKind.Local).AddTicks(5468));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2cfa3083-c8dc-4178-899a-a1342c90082b"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("dd984ddb-ed66-4f1d-883c-5a14d6f6e44b"), 0, null, "74058e94-2fd0-4263-9013-0079ff73f292", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hovanthanh12102002@gmail.com", true, "Van Thanh", "Ho", false, null, "hovanthanh12102002@gmail.com", "thanhhv", "AQAAAAEAACcQAAAAEIfPJ+WSaz/rS/+jGf4yumb5pDavBMvWq+flnEp4ZpmXy9Z+i2rxhsEIeiOGSlItsQ==", "032132131", false, "90fad917-f276-4de0-8565-4f48860c34ee", false, "thanhhv" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 666, DateTimeKind.Local).AddTicks(3696));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3694));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3738));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3742));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3748));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3753));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3757));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3761));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3767));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3771));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3775));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3778));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3907));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3913));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3917));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3921));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3925));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 9, 28, 18, 4, 29, 668, DateTimeKind.Local).AddTicks(3929));
        }
    }
}
