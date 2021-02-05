using Microsoft.EntityFrameworkCore.Migrations;

namespace MMGDH_Blog.Migrations
{
    public partial class b : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Tags_Tagsid",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Tagsid",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Tagsid",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BelongProject",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Articles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "Tagsid",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BelongProject",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Tagsid",
                table: "Projects",
                column: "Tagsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Tags_Tagsid",
                table: "Projects",
                column: "Tagsid",
                principalTable: "Tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
