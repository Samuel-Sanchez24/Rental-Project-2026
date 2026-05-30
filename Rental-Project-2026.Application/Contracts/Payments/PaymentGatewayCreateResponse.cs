using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.Contracts.Payments
{
    public class PaymentGatewayCreateResponse
    {
        public string ProviderReference { get; set; } = string.Empty;
        public string? PaymentUrl { get; set; }
        public PaymentStatus Status { get; set; }
        public string? Message { get; set; }
    }
}
