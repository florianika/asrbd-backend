using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddingUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rules_LocalId",
                table: "Rules");

            migrationBuilder.AlterColumn<string>(
                name: "EntityType",
                table: "Rules",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_LocalId_Version_EntityType",
                table: "Rules",
                columns: new[] { "LocalId", "Version", "EntityType" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rules_LocalId_Version_EntityType",
                table: "Rules");

            migrationBuilder.AlterColumn<string>(
                name: "EntityType",
                table: "Rules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_LocalId",
                table: "Rules",
                column: "LocalId",
                unique: true);
        }
    }
}
