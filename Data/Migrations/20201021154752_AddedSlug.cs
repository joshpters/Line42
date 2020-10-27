using Microsoft.EntityFrameworkCore.Migrations;

namespace CodingBlog.Data.Migrations
{
    public partial class AddedSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Post",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Post");
        }
    }
}
