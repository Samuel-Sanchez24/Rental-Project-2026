using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid ReservationId { get; private set; }
        public Reservation Reservation { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string Provider { get; private set; } = string.Empty;
        public string? ProviderReference { get; private set; }
        public string? PaymentUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? PaidAt { get; private set; }

        private Payment()
        {
        }

        public Payment(Guid reservationId, decimal amount, string provider)
        {
            if (reservationId == Guid.Empty)
                throw new BusinessRulesException("La reserva asociada al pago es requerida.");

            if (amount <= 0)
                throw new BusinessRulesException("El valor del pago debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(provider))
                throw new BusinessRulesException("El proveedor de pago es requerido.");

            Id = Guid.CreateVersion7();
            ReservationId = reservationId;
            Amount = amount;
            Provider = provider.Trim();
            Status = PaymentStatus.Pending;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }

        public void RegisterProviderResponse(
            string providerReference,
            string? paymentUrl,
            PaymentStatus status)
        {
            if (string.IsNullOrWhiteSpace(providerReference))
                throw new BusinessRulesException("La referencia del proveedor de pago es requerida.");

            ProviderReference = providerReference.Trim();
            PaymentUrl = string.IsNullOrWhiteSpace(paymentUrl)
                ? null
                : paymentUrl.Trim();

            ChangeStatus(status);
        }

        public void MarkAsPending()
        {
            if (Status == PaymentStatus.Paid)
                return;

            Status = PaymentStatus.Pending;
            UpdatedAt = DateTime.Now;
        }

        public void MarkAsPaid()
        {
            if (Status == PaymentStatus.Paid)
                return;

            Status = PaymentStatus.Paid;
            UpdatedAt = DateTime.Now;
            PaidAt = DateTime.Now;
        }

        public void MarkAsFailed()
        {
            if (Status == PaymentStatus.Paid)
                return;

            Status = PaymentStatus.Failed;
            UpdatedAt = DateTime.Now;
        }

        public void MarkAsCancelled()
        {
            if (Status == PaymentStatus.Paid)
                return;

            Status = PaymentStatus.Cancelled;
            UpdatedAt = DateTime.Now;
        }

        public void ChangeStatus(PaymentStatus status)
        {
            switch (status)
            {
                case PaymentStatus.Pending:
                    MarkAsPending();
                    break;

                case PaymentStatus.Paid:
                    MarkAsPaid();
                    break;

                case PaymentStatus.Failed:
                    MarkAsFailed();
                    break;

                case PaymentStatus.Cancelled:
                    MarkAsCancelled();
                    break;
            }
        }
    }
}
