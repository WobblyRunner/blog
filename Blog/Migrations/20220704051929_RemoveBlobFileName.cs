using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    public partial class RemoveBlobFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobFileName",
                table: "IMAGES");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlobFileName",
                table: "IMAGES",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
