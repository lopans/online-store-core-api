using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileData_FileID",
                table: "FileData");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "SubCategory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Category",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_Link",
                table: "SubCategory",
                column: "Link",
                unique: true,
                filter: "[Link] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FileData_FileID",
                table: "FileData",
                column: "FileID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_Link",
                table: "Category",
                column: "Link",
                unique: true,
                filter: "[Link] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubCategory_Link",
                table: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_FileData_FileID",
                table: "FileData");

            migrationBuilder.DropIndex(
                name: "IX_Category_Link",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "SubCategory");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_FileData_FileID",
                table: "FileData",
                column: "FileID");
        }
    }
}
