using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relationfieldworkrules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkRules_FieldWorkId",
                table: "FieldWorkRules",
                column: "FieldWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkRules_FieldWorkId_RuleId",
                table: "FieldWorkRules",
                columns: new[] { "FieldWorkId", "RuleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkRules_RuleId",
                table: "FieldWorkRules",
                column: "RuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldWorkRules_FieldWorks_FieldWorkId",
                table: "FieldWorkRules",
                column: "FieldWorkId",
                principalTable: "FieldWorks",
                principalColumn: "FieldWorkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FieldWorkRules_Rules_RuleId",
                table: "FieldWorkRules",
                column: "RuleId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldWorkRules_FieldWorks_FieldWorkId",
                table: "FieldWorkRules");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldWorkRules_Rules_RuleId",
                table: "FieldWorkRules");

            migrationBuilder.DropIndex(
                name: "IX_FieldWorkRules_FieldWorkId",
                table: "FieldWorkRules");

            migrationBuilder.DropIndex(
                name: "IX_FieldWorkRules_FieldWorkId_RuleId",
                table: "FieldWorkRules");

            migrationBuilder.DropIndex(
                name: "IX_FieldWorkRules_RuleId",
                table: "FieldWorkRules");
        }
    }
}
