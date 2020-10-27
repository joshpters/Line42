using Microsoft.EntityFrameworkCore.Migrations;

namespace CodingBlog.Data.Migrations
{
    public partial class AddedIsPublished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Post",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Post");
        }
    }
}
