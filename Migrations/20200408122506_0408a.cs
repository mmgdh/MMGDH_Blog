using Microsoft.EntityFrameworkCore.Migrations;

namespace MMGDH_Blog.Migrations
{
    public partial class _0408a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Projects");
        }
    }
}
