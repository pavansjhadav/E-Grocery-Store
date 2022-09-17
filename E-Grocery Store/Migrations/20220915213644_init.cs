using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Grocery_Store.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroceryCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroceryStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groceries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    VendorId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groceries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groceries_GroceryCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "GroceryCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groceries_GroceryStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "GroceryStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groceries_Users_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" },
                    { 3, "Others" }
                });

            migrationBuilder.InsertData(
                table: "GroceryCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Vegetables" },
                    { 2, "Fruits" },
                    { 3, "Grains" }
                });

            migrationBuilder.InsertData(
                table: "GroceryStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Added" },
                    { 2, "Approved" },
                    { 3, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DOB", "Email", "GenderId", "Name", "Password", "RoleId" },
                values: new object[] { 1, new DateTime(1997, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "bns@mail.com", 1, "Bhuvanesh", "123", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Groceries_CategoryId",
                table: "Groceries",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Groceries_StatusId",
                table: "Groceries",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Groceries_VendorId",
                table: "Groceries",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groceries");

            migrationBuilder.DropTable(
                name: "GroceryCategories");

            migrationBuilder.DropTable(
                name: "GroceryStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
