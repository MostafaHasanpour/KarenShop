using Microsoft.EntityFrameworkCore.Migrations;

namespace KarenShop.Api.Migrations
{
    public partial class userUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "ShopUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ShopUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ShopUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSeller",
                table: "ShopUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "ShopUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductPrices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopUsers_Email",
                table: "ShopUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopUsers_PhoneNumber",
                table: "ShopUsers",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShopUsers_Email",
                table: "ShopUsers");

            migrationBuilder.DropIndex(
                name: "IX_ShopUsers_PhoneNumber",
                table: "ShopUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ShopUsers");

            migrationBuilder.DropColumn(
                name: "IsSeller",
                table: "ShopUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "ShopUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductPrices");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "ShopUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ShopUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
