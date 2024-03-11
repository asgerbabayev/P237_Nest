using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P237_Nest.Data.Migrations
{
    public partial class add_ishover_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHover",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHover",
                table: "ProductImages");
        }
    }
}
