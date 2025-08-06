using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class closeEmailTemplateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailTemplateId",
                table: "FieldWorks",
                newName: "OpenEmailTemplateId");

            migrationBuilder.AddColumn<int>(
                name: "CloseEmailTemplateId",
                table: "FieldWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "CloseEmailTemplateId",
                table: "FieldWorks");

            migrationBuilder.RenameColumn(
                name: "OpenEmailTemplateId",
                table: "FieldWorks",
                newName: "EmailTemplateId");
        }
    }
}
