using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class All : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Rules",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LocalId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
            //        EntityType = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Variable = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        NameAl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        NameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        DescriptionAl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            //        DescriptionEn = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            //        Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        VersionRationale = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            //        Expression = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        QualityAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RuleStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RuleRequirement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            //        Remark = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            //        QualityMessageAl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        QualityMessageEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        UpdatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Rules", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProcessOutputLogs",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        RuleId = table.Column<long>(type: "bigint", nullable: false),
            //        BldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        EntId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        DwlId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Variable = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        QualityAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        QualityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        QualityMessageAl = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        QualityMessageEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ErrorLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProcessOutputLogs", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ProcessOutputLogs_Rules_RuleId",
            //            column: x => x.RuleId,
            //            principalTable: "Rules",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProcessOutputLogs_RuleId",
            //    table: "ProcessOutputLogs",
            //    column: "RuleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Rules_LocalId_Version_EntityType",
            //    table: "Rules",
            //    columns: new[] { "LocalId", "Version", "EntityType" },
            //    unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ProcessOutputLogs");

            //migrationBuilder.DropTable(
            //    name: "Rules");
        }
    }
}
