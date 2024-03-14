using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P237_Nest.Migrations
{
    public partial class update_size_table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sizes_ProductSizeId",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductSizeId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_ProductSizeId",
                table: "Sizes",
                column: "ProductSizeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSizeId",
                table: "Products",
                column: "ProductSizeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sizes_ProductSizeId",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductSizeId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_ProductSizeId",
                table: "Sizes",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSizeId",
                table: "Products",
                column: "ProductSizeId");
        }
    }
}
