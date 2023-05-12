using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityCRUD.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13a5cd2f-3b0c-4e0e-9c6e-b8a16c774dff", null, "Admin", "ADMIN" },
                    { "ec94b5c9-f6c6-4bdf-a426-a9cf400f3078", null, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "QuantityInStock", "Type" },
                values: new object[,]
                {
                    { 1, "Test", "Coffee 1", 86L, 13, "Capu" },
                    { 2, "Test", "Coffee 2", 92L, 10, "Esresso" },
                    { 3, "Test", "Coffee 3", 66L, 2, "Drip" },
                    { 4, "Test", "Coffee 4", 24L, 88, "Drip" },
                    { 5, "Test", "Coffee 5", 47L, 58, "Drip" },
                    { 6, "Test", "Coffee 6", 16L, 44, "Drip" },
                    { 7, "Test", "Coffee 7", 74L, 63, "Red" },
                    { 8, "Test", "Coffee 8", 27L, 18, "Capu" },
                    { 9, "Test", "Coffee 9", 10L, 42, "Esresso" },
                    { 10, "Test", "Coffee 10", 62L, 23, "Capu" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Image", "ProductId" },
                values: new object[,]
                {
                    { 1, "https://picsum.photos/id/1/200/300", 3 },
                    { 2, "https://picsum.photos/id/2/200/300", 7 },
                    { 3, "https://picsum.photos/id/3/200/300", 7 },
                    { 4, "https://picsum.photos/id/4/200/300", 3 },
                    { 5, "https://picsum.photos/id/5/200/300", 4 },
                    { 6, "https://picsum.photos/id/6/200/300", 5 },
                    { 7, "https://picsum.photos/id/7/200/300", 2 },
                    { 8, "https://picsum.photos/id/8/200/300", 1 },
                    { 9, "https://picsum.photos/id/9/200/300", 9 },
                    { 10, "https://picsum.photos/id/10/200/300", 7 },
                    { 11, "https://picsum.photos/id/11/200/300", 10 },
                    { 12, "https://picsum.photos/id/12/200/300", 4 },
                    { 13, "https://picsum.photos/id/13/200/300", 7 },
                    { 14, "https://picsum.photos/id/14/200/300", 5 },
                    { 15, "https://picsum.photos/id/15/200/300", 8 },
                    { 16, "https://picsum.photos/id/16/200/300", 6 },
                    { 17, "https://picsum.photos/id/17/200/300", 10 },
                    { 18, "https://picsum.photos/id/18/200/300", 9 },
                    { 19, "https://picsum.photos/id/19/200/300", 10 },
                    { 20, "https://picsum.photos/id/20/200/300", 1 },
                    { 21, "https://picsum.photos/id/21/200/300", 1 },
                    { 22, "https://picsum.photos/id/22/200/300", 1 },
                    { 23, "https://picsum.photos/id/23/200/300", 1 },
                    { 24, "https://picsum.photos/id/24/200/300", 1 },
                    { 25, "https://picsum.photos/id/25/200/300", 1 },
                    { 26, "https://picsum.photos/id/26/200/300", 7 },
                    { 27, "https://picsum.photos/id/27/200/300", 2 },
                    { 28, "https://picsum.photos/id/28/200/300", 10 },
                    { 29, "https://picsum.photos/id/29/200/300", 3 },
                    { 30, "https://picsum.photos/id/30/200/300", 1 },
                    { 31, "https://picsum.photos/id/31/200/300", 2 },
                    { 32, "https://picsum.photos/id/32/200/300", 1 },
                    { 33, "https://picsum.photos/id/33/200/300", 1 },
                    { 34, "https://picsum.photos/id/34/200/300", 3 },
                    { 35, "https://picsum.photos/id/35/200/300", 9 },
                    { 36, "https://picsum.photos/id/36/200/300", 6 },
                    { 37, "https://picsum.photos/id/37/200/300", 1 },
                    { 38, "https://picsum.photos/id/38/200/300", 1 },
                    { 39, "https://picsum.photos/id/39/200/300", 9 },
                    { 40, "https://picsum.photos/id/40/200/300", 5 },
                    { 41, "https://picsum.photos/id/41/200/300", 5 },
                    { 42, "https://picsum.photos/id/42/200/300", 10 },
                    { 43, "https://picsum.photos/id/43/200/300", 2 },
                    { 44, "https://picsum.photos/id/44/200/300", 6 },
                    { 45, "https://picsum.photos/id/45/200/300", 7 },
                    { 46, "https://picsum.photos/id/46/200/300", 8 },
                    { 47, "https://picsum.photos/id/47/200/300", 5 },
                    { 48, "https://picsum.photos/id/48/200/300", 6 },
                    { 49, "https://picsum.photos/id/49/200/300", 9 },
                    { 50, "https://picsum.photos/id/50/200/300", 7 },
                    { 51, "https://picsum.photos/id/51/200/300", 5 },
                    { 52, "https://picsum.photos/id/52/200/300", 1 },
                    { 53, "https://picsum.photos/id/53/200/300", 6 },
                    { 54, "https://picsum.photos/id/54/200/300", 3 },
                    { 55, "https://picsum.photos/id/55/200/300", 5 },
                    { 56, "https://picsum.photos/id/56/200/300", 3 },
                    { 57, "https://picsum.photos/id/57/200/300", 4 },
                    { 58, "https://picsum.photos/id/58/200/300", 5 },
                    { 59, "https://picsum.photos/id/59/200/300", 7 },
                    { 60, "https://picsum.photos/id/60/200/300", 8 },
                    { 61, "https://picsum.photos/id/61/200/300", 3 },
                    { 62, "https://picsum.photos/id/62/200/300", 5 },
                    { 63, "https://picsum.photos/id/63/200/300", 7 },
                    { 64, "https://picsum.photos/id/64/200/300", 7 },
                    { 65, "https://picsum.photos/id/65/200/300", 10 },
                    { 66, "https://picsum.photos/id/66/200/300", 2 },
                    { 67, "https://picsum.photos/id/67/200/300", 9 },
                    { 68, "https://picsum.photos/id/68/200/300", 3 },
                    { 69, "https://picsum.photos/id/69/200/300", 10 },
                    { 70, "https://picsum.photos/id/70/200/300", 8 },
                    { 71, "https://picsum.photos/id/71/200/300", 5 },
                    { 72, "https://picsum.photos/id/72/200/300", 10 },
                    { 73, "https://picsum.photos/id/73/200/300", 3 },
                    { 74, "https://picsum.photos/id/74/200/300", 2 },
                    { 75, "https://picsum.photos/id/75/200/300", 5 },
                    { 76, "https://picsum.photos/id/76/200/300", 3 },
                    { 77, "https://picsum.photos/id/77/200/300", 8 },
                    { 78, "https://picsum.photos/id/78/200/300", 5 },
                    { 79, "https://picsum.photos/id/79/200/300", 1 },
                    { 80, "https://picsum.photos/id/80/200/300", 3 },
                    { 81, "https://picsum.photos/id/81/200/300", 1 },
                    { 82, "https://picsum.photos/id/82/200/300", 9 },
                    { 83, "https://picsum.photos/id/83/200/300", 2 },
                    { 84, "https://picsum.photos/id/84/200/300", 9 },
                    { 85, "https://picsum.photos/id/85/200/300", 3 },
                    { 86, "https://picsum.photos/id/86/200/300", 1 },
                    { 87, "https://picsum.photos/id/87/200/300", 1 },
                    { 88, "https://picsum.photos/id/88/200/300", 6 },
                    { 89, "https://picsum.photos/id/89/200/300", 1 },
                    { 90, "https://picsum.photos/id/90/200/300", 6 },
                    { 91, "https://picsum.photos/id/91/200/300", 2 },
                    { 92, "https://picsum.photos/id/92/200/300", 1 },
                    { 93, "https://picsum.photos/id/93/200/300", 3 },
                    { 94, "https://picsum.photos/id/94/200/300", 10 },
                    { 95, "https://picsum.photos/id/95/200/300", 6 },
                    { 96, "https://picsum.photos/id/96/200/300", 9 },
                    { 97, "https://picsum.photos/id/97/200/300", 7 },
                    { 98, "https://picsum.photos/id/98/200/300", 9 },
                    { 99, "https://picsum.photos/id/99/200/300", 3 },
                    { 100, "https://picsum.photos/id/100/200/300", 2 },
                    { 101, "https://picsum.photos/id/101/200/300", 5 },
                    { 102, "https://picsum.photos/id/102/200/300", 9 },
                    { 103, "https://picsum.photos/id/103/200/300", 7 },
                    { 104, "https://picsum.photos/id/104/200/300", 6 },
                    { 105, "https://picsum.photos/id/105/200/300", 10 },
                    { 106, "https://picsum.photos/id/106/200/300", 2 },
                    { 107, "https://picsum.photos/id/107/200/300", 6 },
                    { 108, "https://picsum.photos/id/108/200/300", 7 },
                    { 109, "https://picsum.photos/id/109/200/300", 2 },
                    { 110, "https://picsum.photos/id/110/200/300", 8 },
                    { 111, "https://picsum.photos/id/111/200/300", 7 },
                    { 112, "https://picsum.photos/id/112/200/300", 7 },
                    { 113, "https://picsum.photos/id/113/200/300", 3 },
                    { 114, "https://picsum.photos/id/114/200/300", 7 },
                    { 115, "https://picsum.photos/id/115/200/300", 3 },
                    { 116, "https://picsum.photos/id/116/200/300", 2 },
                    { 117, "https://picsum.photos/id/117/200/300", 6 },
                    { 118, "https://picsum.photos/id/118/200/300", 4 },
                    { 119, "https://picsum.photos/id/119/200/300", 9 },
                    { 120, "https://picsum.photos/id/120/200/300", 4 },
                    { 121, "https://picsum.photos/id/121/200/300", 10 },
                    { 122, "https://picsum.photos/id/122/200/300", 5 },
                    { 123, "https://picsum.photos/id/123/200/300", 1 },
                    { 124, "https://picsum.photos/id/124/200/300", 5 },
                    { 125, "https://picsum.photos/id/125/200/300", 1 },
                    { 126, "https://picsum.photos/id/126/200/300", 2 },
                    { 127, "https://picsum.photos/id/127/200/300", 6 },
                    { 128, "https://picsum.photos/id/128/200/300", 8 },
                    { 129, "https://picsum.photos/id/129/200/300", 2 },
                    { 130, "https://picsum.photos/id/130/200/300", 5 },
                    { 131, "https://picsum.photos/id/131/200/300", 9 },
                    { 132, "https://picsum.photos/id/132/200/300", 4 },
                    { 133, "https://picsum.photos/id/133/200/300", 7 },
                    { 134, "https://picsum.photos/id/134/200/300", 1 },
                    { 135, "https://picsum.photos/id/135/200/300", 8 },
                    { 136, "https://picsum.photos/id/136/200/300", 4 },
                    { 137, "https://picsum.photos/id/137/200/300", 3 },
                    { 138, "https://picsum.photos/id/138/200/300", 4 },
                    { 139, "https://picsum.photos/id/139/200/300", 4 },
                    { 140, "https://picsum.photos/id/140/200/300", 1 },
                    { 141, "https://picsum.photos/id/141/200/300", 8 },
                    { 142, "https://picsum.photos/id/142/200/300", 7 },
                    { 143, "https://picsum.photos/id/143/200/300", 5 },
                    { 144, "https://picsum.photos/id/144/200/300", 4 },
                    { 145, "https://picsum.photos/id/145/200/300", 3 },
                    { 146, "https://picsum.photos/id/146/200/300", 8 },
                    { 147, "https://picsum.photos/id/147/200/300", 3 },
                    { 148, "https://picsum.photos/id/148/200/300", 5 },
                    { 149, "https://picsum.photos/id/149/200/300", 7 },
                    { 150, "https://picsum.photos/id/150/200/300", 1 },
                    { 151, "https://picsum.photos/id/151/200/300", 8 },
                    { 152, "https://picsum.photos/id/152/200/300", 2 },
                    { 153, "https://picsum.photos/id/153/200/300", 6 },
                    { 154, "https://picsum.photos/id/154/200/300", 6 },
                    { 155, "https://picsum.photos/id/155/200/300", 1 },
                    { 156, "https://picsum.photos/id/156/200/300", 9 },
                    { 157, "https://picsum.photos/id/157/200/300", 9 },
                    { 158, "https://picsum.photos/id/158/200/300", 5 },
                    { 159, "https://picsum.photos/id/159/200/300", 2 },
                    { 160, "https://picsum.photos/id/160/200/300", 3 },
                    { 161, "https://picsum.photos/id/161/200/300", 5 },
                    { 162, "https://picsum.photos/id/162/200/300", 6 },
                    { 163, "https://picsum.photos/id/163/200/300", 6 },
                    { 164, "https://picsum.photos/id/164/200/300", 2 },
                    { 165, "https://picsum.photos/id/165/200/300", 4 },
                    { 166, "https://picsum.photos/id/166/200/300", 1 },
                    { 167, "https://picsum.photos/id/167/200/300", 2 },
                    { 168, "https://picsum.photos/id/168/200/300", 1 },
                    { 169, "https://picsum.photos/id/169/200/300", 5 },
                    { 170, "https://picsum.photos/id/170/200/300", 10 },
                    { 171, "https://picsum.photos/id/171/200/300", 2 },
                    { 172, "https://picsum.photos/id/172/200/300", 9 },
                    { 173, "https://picsum.photos/id/173/200/300", 6 },
                    { 174, "https://picsum.photos/id/174/200/300", 6 },
                    { 175, "https://picsum.photos/id/175/200/300", 9 },
                    { 176, "https://picsum.photos/id/176/200/300", 5 },
                    { 177, "https://picsum.photos/id/177/200/300", 5 },
                    { 178, "https://picsum.photos/id/178/200/300", 4 },
                    { 179, "https://picsum.photos/id/179/200/300", 8 },
                    { 180, "https://picsum.photos/id/180/200/300", 7 },
                    { 181, "https://picsum.photos/id/181/200/300", 2 },
                    { 182, "https://picsum.photos/id/182/200/300", 10 },
                    { 183, "https://picsum.photos/id/183/200/300", 2 },
                    { 184, "https://picsum.photos/id/184/200/300", 10 },
                    { 185, "https://picsum.photos/id/185/200/300", 5 },
                    { 186, "https://picsum.photos/id/186/200/300", 7 },
                    { 187, "https://picsum.photos/id/187/200/300", 3 },
                    { 188, "https://picsum.photos/id/188/200/300", 8 },
                    { 189, "https://picsum.photos/id/189/200/300", 3 },
                    { 190, "https://picsum.photos/id/190/200/300", 4 },
                    { 191, "https://picsum.photos/id/191/200/300", 4 },
                    { 192, "https://picsum.photos/id/192/200/300", 10 },
                    { 193, "https://picsum.photos/id/193/200/300", 7 },
                    { 194, "https://picsum.photos/id/194/200/300", 1 },
                    { 195, "https://picsum.photos/id/195/200/300", 1 },
                    { 196, "https://picsum.photos/id/196/200/300", 4 },
                    { 197, "https://picsum.photos/id/197/200/300", 2 },
                    { 198, "https://picsum.photos/id/198/200/300", 1 },
                    { 199, "https://picsum.photos/id/199/200/300", 3 },
                    { 200, "https://picsum.photos/id/200/200/300", 4 },
                    { 201, "https://picsum.photos/id/201/200/300", 7 },
                    { 202, "https://picsum.photos/id/202/200/300", 3 },
                    { 203, "https://picsum.photos/id/203/200/300", 8 },
                    { 204, "https://picsum.photos/id/204/200/300", 2 },
                    { 205, "https://picsum.photos/id/205/200/300", 2 },
                    { 206, "https://picsum.photos/id/206/200/300", 3 },
                    { 207, "https://picsum.photos/id/207/200/300", 8 },
                    { 208, "https://picsum.photos/id/208/200/300", 5 },
                    { 209, "https://picsum.photos/id/209/200/300", 2 },
                    { 210, "https://picsum.photos/id/210/200/300", 1 },
                    { 211, "https://picsum.photos/id/211/200/300", 4 },
                    { 212, "https://picsum.photos/id/212/200/300", 2 },
                    { 213, "https://picsum.photos/id/213/200/300", 6 },
                    { 214, "https://picsum.photos/id/214/200/300", 7 },
                    { 215, "https://picsum.photos/id/215/200/300", 6 },
                    { 216, "https://picsum.photos/id/216/200/300", 7 },
                    { 217, "https://picsum.photos/id/217/200/300", 9 },
                    { 218, "https://picsum.photos/id/218/200/300", 8 },
                    { 219, "https://picsum.photos/id/219/200/300", 2 },
                    { 220, "https://picsum.photos/id/220/200/300", 6 },
                    { 221, "https://picsum.photos/id/221/200/300", 2 },
                    { 222, "https://picsum.photos/id/222/200/300", 3 },
                    { 223, "https://picsum.photos/id/223/200/300", 4 },
                    { 224, "https://picsum.photos/id/224/200/300", 6 },
                    { 225, "https://picsum.photos/id/225/200/300", 8 },
                    { 226, "https://picsum.photos/id/226/200/300", 2 },
                    { 227, "https://picsum.photos/id/227/200/300", 2 },
                    { 228, "https://picsum.photos/id/228/200/300", 2 },
                    { 229, "https://picsum.photos/id/229/200/300", 3 },
                    { 230, "https://picsum.photos/id/230/200/300", 3 },
                    { 231, "https://picsum.photos/id/231/200/300", 4 },
                    { 232, "https://picsum.photos/id/232/200/300", 9 },
                    { 233, "https://picsum.photos/id/233/200/300", 1 },
                    { 234, "https://picsum.photos/id/234/200/300", 6 },
                    { 235, "https://picsum.photos/id/235/200/300", 2 },
                    { 236, "https://picsum.photos/id/236/200/300", 3 },
                    { 237, "https://picsum.photos/id/237/200/300", 6 },
                    { 238, "https://picsum.photos/id/238/200/300", 2 },
                    { 239, "https://picsum.photos/id/239/200/300", 8 },
                    { 240, "https://picsum.photos/id/240/200/300", 3 },
                    { 241, "https://picsum.photos/id/241/200/300", 7 },
                    { 242, "https://picsum.photos/id/242/200/300", 2 },
                    { 243, "https://picsum.photos/id/243/200/300", 3 },
                    { 244, "https://picsum.photos/id/244/200/300", 8 },
                    { 245, "https://picsum.photos/id/245/200/300", 4 },
                    { 246, "https://picsum.photos/id/246/200/300", 5 },
                    { 247, "https://picsum.photos/id/247/200/300", 9 },
                    { 248, "https://picsum.photos/id/248/200/300", 2 },
                    { 249, "https://picsum.photos/id/249/200/300", 2 },
                    { 250, "https://picsum.photos/id/250/200/300", 4 },
                    { 251, "https://picsum.photos/id/251/200/300", 1 },
                    { 252, "https://picsum.photos/id/252/200/300", 3 },
                    { 253, "https://picsum.photos/id/253/200/300", 7 },
                    { 254, "https://picsum.photos/id/254/200/300", 8 },
                    { 255, "https://picsum.photos/id/255/200/300", 2 },
                    { 256, "https://picsum.photos/id/256/200/300", 3 },
                    { 257, "https://picsum.photos/id/257/200/300", 8 },
                    { 258, "https://picsum.photos/id/258/200/300", 9 },
                    { 259, "https://picsum.photos/id/259/200/300", 7 },
                    { 260, "https://picsum.photos/id/260/200/300", 7 },
                    { 261, "https://picsum.photos/id/261/200/300", 10 },
                    { 262, "https://picsum.photos/id/262/200/300", 8 },
                    { 263, "https://picsum.photos/id/263/200/300", 10 },
                    { 264, "https://picsum.photos/id/264/200/300", 8 },
                    { 265, "https://picsum.photos/id/265/200/300", 8 },
                    { 266, "https://picsum.photos/id/266/200/300", 4 },
                    { 267, "https://picsum.photos/id/267/200/300", 1 },
                    { 268, "https://picsum.photos/id/268/200/300", 4 },
                    { 269, "https://picsum.photos/id/269/200/300", 3 },
                    { 270, "https://picsum.photos/id/270/200/300", 10 },
                    { 271, "https://picsum.photos/id/271/200/300", 2 },
                    { 272, "https://picsum.photos/id/272/200/300", 1 },
                    { 273, "https://picsum.photos/id/273/200/300", 1 },
                    { 274, "https://picsum.photos/id/274/200/300", 1 },
                    { 275, "https://picsum.photos/id/275/200/300", 1 },
                    { 276, "https://picsum.photos/id/276/200/300", 6 },
                    { 277, "https://picsum.photos/id/277/200/300", 8 },
                    { 278, "https://picsum.photos/id/278/200/300", 5 },
                    { 279, "https://picsum.photos/id/279/200/300", 10 },
                    { 280, "https://picsum.photos/id/280/200/300", 6 },
                    { 281, "https://picsum.photos/id/281/200/300", 4 },
                    { 282, "https://picsum.photos/id/282/200/300", 3 },
                    { 283, "https://picsum.photos/id/283/200/300", 2 },
                    { 284, "https://picsum.photos/id/284/200/300", 9 },
                    { 285, "https://picsum.photos/id/285/200/300", 2 },
                    { 286, "https://picsum.photos/id/286/200/300", 8 },
                    { 287, "https://picsum.photos/id/287/200/300", 3 },
                    { 288, "https://picsum.photos/id/288/200/300", 8 },
                    { 289, "https://picsum.photos/id/289/200/300", 3 },
                    { 290, "https://picsum.photos/id/290/200/300", 6 },
                    { 291, "https://picsum.photos/id/291/200/300", 3 },
                    { 292, "https://picsum.photos/id/292/200/300", 5 },
                    { 293, "https://picsum.photos/id/293/200/300", 1 },
                    { 294, "https://picsum.photos/id/294/200/300", 9 },
                    { 295, "https://picsum.photos/id/295/200/300", 8 },
                    { 296, "https://picsum.photos/id/296/200/300", 4 },
                    { 297, "https://picsum.photos/id/297/200/300", 5 },
                    { 298, "https://picsum.photos/id/298/200/300", 10 },
                    { 299, "https://picsum.photos/id/299/200/300", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
