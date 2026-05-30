using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.UseCases.Payments.Commands.UpdatePaymentStatus
{
    public class UpdatePaymentStatusCommand : IRequest
    {
        public Guid? PaymentId { get; set; }
        public string? ProviderReference { get; set; }
        public PaymentStatus Status { get; set; }
        public string? CurrentUserId { get; set; }
        public bool ValidateProviderCallback { get; set; }
        public string? Signature { get; set; }
        public string? Payload { get; set; }
    }
}
