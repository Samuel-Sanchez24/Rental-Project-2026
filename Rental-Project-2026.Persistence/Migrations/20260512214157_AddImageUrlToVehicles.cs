using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental_Project_2026.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Vehicles",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Vehicles");
        }
    }
}
