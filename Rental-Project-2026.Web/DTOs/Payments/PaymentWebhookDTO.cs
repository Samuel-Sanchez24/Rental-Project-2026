using Rental_Project_2026.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rental_Project_2026.Web.DTOs.Payments
{
    public class PaymentWebhookDTO
    {
        [Required]
        public string ProviderReference { get; set; } = string.Empty;

        [Required]
        public PaymentStatus Status { get; set; }

        public string? Signature { get; set; }
        public string? Payload { get; set; }
    }
}
