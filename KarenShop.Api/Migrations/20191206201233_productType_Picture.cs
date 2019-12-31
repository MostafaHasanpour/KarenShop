using Microsoft.EntityFrameworkCore.Migrations;

namespace KarenShop.Api.Migrations
{
    public partial class productType_Picture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureAddresses");

            migrationBuilder.AddColumn<string>(
                name: "ProductTypePictureUri",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductPictureAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uri = table.Column<string>(nullable: true),
                    ShowInOffer = table.Column<bool>(nullable: false),
                    ProductId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPictureAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPictureAddress_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictureAddress_ProductId",
                table: "ProductPictureAddress",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPictureAddress");

            migrationBuilder.DropColumn(
                name: "ProductTypePictureUri",
                table: "ProductTypes");

            migrationBuilder.CreateTable(
                name: "PictureAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    ShowInOffer = table.Column<bool>(type: "bit", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PictureAddresses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PictureAddresses_ProductId",
                table: "PictureAddresses",
                column: "ProductId");
        }
    }
}
