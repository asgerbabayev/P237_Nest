using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P237_Nest.Migrations
{
    public partial class update_size_table4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "SizesId",
                table: "ProductSizes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "ProductSizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizesId",
                table: "ProductSizes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
