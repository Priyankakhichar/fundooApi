using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class fundoo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "NotesModels",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Reminder",
                table: "NotesModels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "NotesModels");

            migrationBuilder.DropColumn(
                name: "Reminder",
                table: "NotesModels");
        }
    }
}
