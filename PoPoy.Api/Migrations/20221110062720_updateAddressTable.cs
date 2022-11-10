using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoPoy.Api.Migrations
{
    public partial class updateAddressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Addresses",
                newName: "Ward");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Addresses",
                newName: "District");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 254, DateTimeKind.Local).AddTicks(1539));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2554));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2583));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2591));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2594));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2597));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2599));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2603));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2608));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2611));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2613));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2616));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2619));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2622));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2625));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2628));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2632));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 11, 10, 13, 27, 19, 255, DateTimeKind.Local).AddTicks(2635));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ward",
                table: "Addresses",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Addresses",
                newName: "Country");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 720, DateTimeKind.Local).AddTicks(9071));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2071));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2111));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2115));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2117));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2120));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2123));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2125));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2130));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2135));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2138));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2141));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2144));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2152));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 11, 7, 17, 16, 16, 722, DateTimeKind.Local).AddTicks(2155));
        }
    }
}
