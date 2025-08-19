using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FieldworkProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FieldWorkProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldworkId = table.Column<int>(type: "int", nullable: false),
                    MunicipalityCode = table.Column<int>(type: "int", nullable: false),
                    ApprovedBuildings = table.Column<int>(type: "int", nullable: false),
                    FieldworkBuildings = table.Column<int>(type: "int", nullable: false),
                    ProgressPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldWorkProgress", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkProgress_FieldworkId",
                table: "FieldWorkProgress",
                column: "FieldworkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldWorkProgress");

        }
    }
}
