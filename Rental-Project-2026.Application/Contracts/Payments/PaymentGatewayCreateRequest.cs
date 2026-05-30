namespace Rental_Project_2026.Application.Contracts.Payments
{
    public class PaymentGatewayCreateRequest
    {
        public Guid PaymentId { get; set; }
        public Guid ReservationId { get; set; }
        public decimal Amount { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ReturnUrl { get; set; }
        public string? WebhookUrl { get; set; }
    }
}
