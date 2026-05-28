using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental_Project_2026.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDriverLicenseNumberToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriverLicenseNumber",
                table: "Reservations",
                newName: "DriverLicenseCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriverLicenseCategories",
                table: "Reservations",
                newName: "DriverLicenseNumber");
        }
    }
}
