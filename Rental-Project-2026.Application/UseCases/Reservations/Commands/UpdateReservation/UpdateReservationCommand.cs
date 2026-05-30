using Rental_Project_2026.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommand : IRequest
    {
        public Guid Id { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public ReservationStatus Status { get; set; }

        public bool IsAdmin { get; set; }

        public string CustomerFullName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public List<string> DriverLicenseCategories { get; set; } = new();
        public DateTime DriverLicenseExpirationDate { get; set; }
        public bool RequiresSpecialAssistance { get; set; }
        public string? AssistanceNotes { get; set; }
    }
}
