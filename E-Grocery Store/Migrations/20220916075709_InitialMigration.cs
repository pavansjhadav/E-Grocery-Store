using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Grocery_Store.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "pavans@mail.com", "Pavan" });
        }

    }
}
