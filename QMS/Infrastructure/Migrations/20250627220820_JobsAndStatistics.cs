using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobsAndStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldWorkId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_FieldWorks_FieldWorkId",
                        column: x => x.FieldWorkId,
                        principalTable: "FieldWorks",
                        principalColumn: "FieldWorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RuleStatistics = table.Column<int>(type: "int", nullable: false),
                    BldStatistics = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistics_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_FieldWorkId",
                table: "Jobs",
                column: "FieldWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Id",
                table: "Jobs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_JobId",
                table: "Statistics",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
