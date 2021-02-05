using Microsoft.EntityFrameworkCore.Migrations;

namespace MMGDH_Blog.Migrations
{
    public partial class _0409a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImagePath",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImagePath",
                table: "Articles");
        }
    }
}
