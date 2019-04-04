using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestMigrate",
                table: "TestObject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestMigrate",
                table: "TestObject",
                nullable: true);
        }
    }
}
