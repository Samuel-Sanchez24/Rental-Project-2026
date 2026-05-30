using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.Contracts.Payments
{
    public class PaymentGatewayWebhookValidationRequest
    {
        public string ProviderReference { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; }
        public string? Signature { get; set; }
        public string? Payload { get; set; }
    }
}
