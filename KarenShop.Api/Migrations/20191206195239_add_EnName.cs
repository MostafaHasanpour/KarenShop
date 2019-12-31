using Microsoft.EntityFrameworkCore.Migrations;

namespace KarenShop.Api.Migrations
{
    public partial class add_EnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnName",
                table: "SubCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnName",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnName",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "EnName",
                table: "Categories");
        }
    }
}
