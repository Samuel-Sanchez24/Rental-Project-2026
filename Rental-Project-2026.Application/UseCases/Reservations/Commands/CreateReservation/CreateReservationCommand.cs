using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest<Guid>
    {
        public required Guid VehicleId { get; set; }
        public required string UserId { get; set; } = null!;
        public required DateTime RentDate { get; set; }
        public required DateTime ReturnDate { get; set; }

        public required string CustomerFullName { get; set; } = null!;
        public required string DocumentNumber { get; set; } = null!;
        public required string PhoneNumber { get; set; } = null!;
        public required string Email { get; set; } = null!;
        public required DateTime BirthDate { get; set; }

        public List<string> DriverLicenseCategories { get; set; } = new();
        public required DateTime DriverLicenseExpirationDate { get; set; }

        public bool RequiresSpecialAssistance { get; set; }
        public string? AssistanceNotes { get; set; }
    }
}
