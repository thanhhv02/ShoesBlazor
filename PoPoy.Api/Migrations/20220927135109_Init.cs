using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoPoy.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvatarPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PayPalPayment = table.Column<double>(type: "float", nullable: false),
                    orderReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInCategories", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductInCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductQuantities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQuantities_ProductColors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "ProductColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQuantities_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQuantities_ProductSizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "ProductSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("fb1bf09c-fc44-4cff-9fbe-836e025ca9d9"), 0, null, "1f9b2795-72bb-4623-af50-057b6984a518", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hovanthanh12102002@gmail.com", true, "Van Thanh", "Ho", false, null, "hovanthanh12102002@gmail.com", "thanhhv", "AQAAAAEAACcQAAAAEIfPJ+WSaz/rS/+jGf4yumb5pDavBMvWq+flnEp4ZpmXy9Z+i2rxhsEIeiOGSlItsQ==", "032132131", false, "d8aafb3a-3b3e-49ea-b9eb-1ee231a4ef99", false, "thanhhv" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "SortOrder", "Status", "Url" },
                values: new object[,]
                {
                    { 3, "Giày Nike", 1, 1, "nike-shoes" },
                    { 1, "Giày Adidas", 1, 1, "adidas-shoes" },
                    { 2, "Giày Jordan", 1, 1, "jordan-shoes" }
                });

            migrationBuilder.InsertData(
                table: "ProductColors",
                columns: new[] { "Id", "Color" },
                values: new object[,]
                {
                    { 3, "Green" },
                    { 2, "Red" },
                    { 1, "Yellow" }
                });

            migrationBuilder.InsertData(
                table: "ProductSizes",
                columns: new[] { "Id", "Size" },
                values: new object[,]
                {
                    { 2, 39 },
                    { 1, 38 },
                    { 3, 40 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DateCreated", "Description", "OriginalPrice", "Price", "Quantity", "Stock", "Title", "Views" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 9, 27, 20, 51, 8, 161, DateTimeKind.Local).AddTicks(6222), "ADIDAS ALPHABOOST “CORE BLACK”", 2150000m, 2150000m, 1000, 0, "ADIDAS ALPHABOOST “CORE BLACK”", 0 },
                    { 18, 3, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2135), "NIKE AIR FORCE 1 LOW ’07 ESSENTIAL WHITE METALLIC SILVER BLACK ", 3200000m, 3200000m, 1000, 0, "NIKE AIR FORCE 1 LOW ’07 ESSENTIAL WHITE METALLIC SILVER BLACK ", 0 },
                    { 17, 3, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2132), "NIKE AIR FORCE 1 GS WHITE UNIVERSITY RED ", 2850000m, 2850000m, 1000, 0, "NIKE AIR FORCE 1 GS WHITE UNIVERSITY RED ", 0 },
                    { 16, 3, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2127), "NIKE AIR FORCE 1 GS LOW WHITE PINK FOAM ", 2950000m, 2950000m, 1000, 0, "NIKE AIR FORCE 1 GS LOW WHITE PINK FOAM ", 0 },
                    { 15, 3, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2124), "NIKE AIR FORCE 1 LOW BY YOU CUSTOM – GUCCI ", 3950000m, 3950000m, 1000, 0, "NIKE AIR FORCE 1 LOW BY YOU CUSTOM – GUCCI ", 0 },
                    { 14, 3, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2119), "GIÀY NIKE DUNK LOW DISRUPT 2 ‘MALACHITE’ ", 4850000m, 4850000m, 1000, 0, "GIÀY NIKE DUNK LOW DISRUPT 2 ‘MALACHITE’ ", 0 },
                    { 13, 3, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2115), "CR7 X AIR MAX 97 GS ‘PORTUGAL PATCHWORK’ ", 4300000m, 4300000m, 1000, 0, "CR7 X AIR MAX 97 GS ‘PORTUGAL PATCHWORK’ ", 0 },
                    { 10, 2, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2104), "AIR JORDAN 1 HIGH ZOOM ‘CANYON RUST’ ", 5550000m, 5550000m, 1000, 0, "AIR JORDAN 1 HIGH ZOOM ‘CANYON RUST’ ", 0 },
                    { 9, 2, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2101), "AIR JORDAN 1 HIGH RETRO ‘HERITAGE’ GS ", 4850000m, 4850000m, 1000, 0, "AIR JORDAN 1 HIGH RETRO ‘HERITAGE’ GS ", 0 },
                    { 8, 2, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2098), "AIR JORDAN 1 HIGH OG “BUBBLE GUM”", 6450000m, 6450000m, 1000, 0, "AIR JORDAN 1 HIGH OG “BUBBLE GUM”", 0 },
                    { 7, 2, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2095), "AIR JORDAN 1 HIGH ‘BORDEAUX’", 6100000m, 6100000m, 1000, 0, "AIR JORDAN 1 HIGH ‘BORDEAUX’", 0 },
                    { 6, 1, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2091), "ADIDAS ULTRA BOOST 20 NASA CLOUD WHITE ", 2550000m, 2550000m, 1000, 0, "ADIDAS ULTRA BOOST 20 NASA CLOUD WHITE ", 0 },
                    { 5, 1, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2089), "ADIDAS NMD R1 TOKYO DRAGON", 1850000m, 1850000m, 1000, 0, "ADIDAS NMD R1 TOKYO DRAGON", 0 },
                    { 4, 1, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2085), "ADIDAS SUPERSTAR OG ‘VINTAGE WHITE’", 1650000m, 1650000m, 1000, 0, "ADIDAS SUPERSTAR OG ‘VINTAGE WHITE’", 0 },
                    { 3, 1, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2081), "ADIDAS SEAN WOTHERSPOON X SUPERSTAR ‘SUPER EARTH – BLACK’", 3250000m, 3250000m, 1000, 0, "ADIDAS SEAN WOTHERSPOON X SUPERSTAR ‘SUPER EARTH – BLACK’", 0 },
                    { 2, 1, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2035), "ADIDAS NMD R1 SERIAL PACK METAL GREY", 1650000m, 1650000m, 1000, 0, "ADIDAS NMD R1 SERIAL PACK METAL GREY", 0 },
                    { 12, 2, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2111), "AIR JORDAN 1 LOW GS RUSH BLUE BRILL ", 4350000m, 4350000m, 1000, 0, "AIR JORDAN 1 LOW GS RUSH BLUE BRILL ", 0 },
                    { 11, 2, new DateTime(2022, 9, 27, 20, 51, 8, 163, DateTimeKind.Local).AddTicks(2107), "AIR JORDAN 1 LOW GS TRIPLE WHITE ", 3850000m, 3850000m, 1000, 0, "AIR JORDAN 1 LOW GS TRIPLE WHITE ", 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductInCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 17 },
                    { 3, 16 },
                    { 3, 15 },
                    { 3, 14 },
                    { 3, 13 },
                    { 2, 12 },
                    { 2, 11 },
                    { 2, 10 },
                    { 2, 8 },
                    { 2, 7 },
                    { 1, 6 },
                    { 1, 5 },
                    { 1, 4 },
                    { 1, 3 },
                    { 1, 2 },
                    { 2, 9 }
                });

            migrationBuilder.InsertData(
                table: "ProductQuantities",
                columns: new[] { "Id", "ColorId", "ProductId", "SizeId" },
                values: new object[,]
                {
                    { 2, 2, 1, 2 },
                    { 1, 1, 1, 1 },
                    { 3, 3, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_AddressId",
                table: "Carts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCategories_ProductId",
                table: "ProductInCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantities_ColorId",
                table: "ProductQuantities",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantities_ProductId",
                table: "ProductQuantities",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantities_SizeId",
                table: "ProductQuantities",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductInCategories");

            migrationBuilder.DropTable(
                name: "ProductQuantities");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
