using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    public partial class CreateNewImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "BLOG_POSTS");

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "BLOG_POSTS",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Thumbnail_ImageID",
                table: "BLOG_POSTS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IMAGES",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageBlob = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGES", x => x.ImageID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BLOG_POSTS_Thumbnail_ImageID",
                table: "BLOG_POSTS",
                column: "Thumbnail_ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_BLOG_POSTS_IMAGES_Thumbnail_ImageID",
                table: "BLOG_POSTS",
                column: "Thumbnail_ImageID",
                principalTable: "IMAGES",
                principalColumn: "ImageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BLOG_POSTS_IMAGES_Thumbnail_ImageID",
                table: "BLOG_POSTS");

            migrationBuilder.DropTable(
                name: "IMAGES");

            migrationBuilder.DropIndex(
                name: "IX_BLOG_POSTS_Thumbnail_ImageID",
                table: "BLOG_POSTS");

            migrationBuilder.DropColumn(
                name: "Thumbnail_ImageID",
                table: "BLOG_POSTS");

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "BLOG_POSTS",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600);

            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "BLOG_POSTS",
                type: "varbinary(max)",
                maxLength: 16908544,
                nullable: true);
        }
    }
}
