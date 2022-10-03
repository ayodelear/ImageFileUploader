using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageFileUploaderAPI.Migrations
{
    public partial class Location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoreLocation",
                table: "BlobDtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreLocation",
                table: "BlobDtos");
        }
    }
}
