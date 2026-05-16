using Rental_Project_2026.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<Guid>
    {
        public required string Plate { get; set; }
        public required string Model { get; set; }
        public required string Brand { get; set; }
        public required string Color { get; set; }
        public required int Year { get; set; }
        public required decimal DailyPrice { get; set; } = 0;
        public required VehicleStatus Status { get; set; }
        public string? ImageUrl { get; set; }
        public required Guid BranchId { get; set; }
    }
}
