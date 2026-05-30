using Rental_Project_2026.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Queries.GetReservationById
{
    public class ReservationDetailDTO
    {
        public Guid Id { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public int Days { get; set; }

        public decimal DailyPriceAtBooking { get; set; }
        public decimal TotalPrice { get; set; }

        public ReservationStatus Status { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public Guid? PaymentId { get; set; }
        public string? PaymentProvider { get; set; }
        public string? PaymentProviderReference { get; set; }
        public bool CanPayNow { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid VehicleId { get; set; }
        public string VehiclePlate { get; set; } = string.Empty;
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string? VehicleImageUrl { get; set; }

        public string CustomerFullName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public string DriverLicenseCategories { get; set; } = string.Empty;
        public DateTime DriverLicenseExpirationDate { get; set; }

        public bool RequiresSpecialAssistance { get; set; }
        public string? AssistanceNotes { get; set; }

        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string BranchCity { get; set; } = string.Empty;
        public string BranchAddress { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
    }
}
