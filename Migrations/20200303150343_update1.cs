using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMGDH_Blog.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BelongProject",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Articles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "KnowledgePoints",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Synopsis",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "BelongProject",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "KnowledgePoints",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Synopsis",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
