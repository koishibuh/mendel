using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mendel.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedUnisexToSpecies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdultUnisex",
                table: "Species",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "JuvenileUnisex",
                table: "Species",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultUnisex",
                table: "Species");

            migrationBuilder.DropColumn(
                name: "JuvenileUnisex",
                table: "Species");
        }
    }
}
