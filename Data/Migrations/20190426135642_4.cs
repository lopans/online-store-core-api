using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "SubCategory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Category",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "SubCategory");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Category");
        }
    }
}
