using Rental_Project_2026.Domain.Entities.Branches;
using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleById
{
    public class VehicleDetailDTO
    {
        public Guid Id { get; set; }
        public string Plate { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Color { get; set; } = null!;
        public int Year { get; set; }
        public decimal DailyPrice { get; set; }
        public VehicleStatus Status { get; set; }
        public string? ImageUrl { get; set; }

        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = null!;
        public string BranchCity { get; set; } = null!;
        public string BranchAddress { get; set; } = null!;
    }
}
