using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.UseCases.Payments.Queries.GetPaymentById
{
    public class PaymentDetailDTO
    {
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string? ProviderReference { get; set; }
        public string? PaymentUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public string VehiclePlate { get; set; } = string.Empty;
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string CustomerFullName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public bool IsMockProvider { get; set; }
    }
}
