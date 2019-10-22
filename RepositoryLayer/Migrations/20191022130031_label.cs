using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class label : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotesModelId",
                table: "LabelModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabelModels_NotesModelId",
                table: "LabelModels",
                column: "NotesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabelModels_NotesModels_NotesModelId",
                table: "LabelModels",
                column: "NotesModelId",
                principalTable: "NotesModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelModels_NotesModels_NotesModelId",
                table: "LabelModels");

            migrationBuilder.DropIndex(
                name: "IX_LabelModels_NotesModelId",
                table: "LabelModels");

            migrationBuilder.DropColumn(
                name: "NotesModelId",
                table: "LabelModels");
        }
    }
}
