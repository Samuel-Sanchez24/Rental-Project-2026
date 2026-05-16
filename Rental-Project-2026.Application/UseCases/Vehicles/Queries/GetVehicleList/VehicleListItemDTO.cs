using Rental_Project_2026.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleList
{
    public class VehicleListItemDTO
    {
        public Guid Id { get; set; }
        public string Plate { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Color { get; set; } = null!;
        public int Year { get; set; }
        public decimal DailyPrice { get; set; }
        public VehicleStatus Status { get; set; }
        public string? ImageUrl { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string BranchCity { get; set; } = string.Empty;
    }
}
