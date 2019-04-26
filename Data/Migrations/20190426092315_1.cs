using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ImageID",
                table: "Item",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_FileData_ImageID",
                table: "Item",
                column: "ImageID",
                principalTable: "FileData",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_FileData_ImageID",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ImageID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Item");
        }
    }
}
