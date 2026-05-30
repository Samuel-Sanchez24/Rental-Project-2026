using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.UseCases.Payments.Commands.CreatePaymentForReservation
{
    public class CreatePaymentForReservationResult
    {
        public Guid PaymentId { get; set; }
        public string ProviderReference { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; }
        public bool ReusedExistingPayment { get; set; }
        public string? Message { get; set; }
    }
}
