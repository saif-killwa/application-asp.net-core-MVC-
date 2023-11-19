using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocomecApp.Migrations
{
    public partial class timelekdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Annee",
                table: "Rendu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mois",
                table: "Rendu",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Annee",
                table: "Rendu");

            migrationBuilder.DropColumn(
                name: "Mois",
                table: "Rendu");
        }
    }
}
