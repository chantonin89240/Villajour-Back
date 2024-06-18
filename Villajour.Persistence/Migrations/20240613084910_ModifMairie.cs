using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Villajour.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifMairie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Mairies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Mairies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Mairies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Mairies");
        }
    }
}
