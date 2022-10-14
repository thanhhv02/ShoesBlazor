using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoPoy.Api.Migrations
{
    public partial class addReviewAverage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "ReviewAverage",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 888, DateTimeKind.Local).AddTicks(6155));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2241));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2263));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2267));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2274));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2277));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2279));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2282));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2284));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2286));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2289));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2291));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2293));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2296));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2299));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2301));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2303));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 10, 13, 20, 54, 0, 890, DateTimeKind.Local).AddTicks(2306));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewAverage",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 520, DateTimeKind.Local).AddTicks(3605), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(3982), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4014), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4017), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4020), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4023), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4025), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4027), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4029), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4031), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4034), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4036), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4038), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4041), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4043), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4045), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4047), 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2022, 10, 11, 9, 33, 24, 521, DateTimeKind.Local).AddTicks(4049), 1000 });
        }
    }
}
