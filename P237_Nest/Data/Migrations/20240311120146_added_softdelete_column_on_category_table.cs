using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P237_Nest.Data.Migrations
{
    public partial class added_softdelete_column_on_category_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDelete",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDelete",
                table: "Categories");
        }
    }
}
