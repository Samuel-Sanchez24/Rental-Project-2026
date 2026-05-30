using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IPaymentsRepository : IRepository<Payment>
    {
        Task<Payment?> GetPaymentByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<Payment?> GetLatestByReservationIdAsync(
            Guid reservationId,
            CancellationToken cancellationToken = default);

        Task<Payment?> GetPendingByReservationIdAsync(
            Guid reservationId,
            CancellationToken cancellationToken = default);

        Task<Payment?> GetPaidByReservationIdAsync(
            Guid reservationId,
            CancellationToken cancellationToken = default);

        Task<Payment?> GetByProviderReferenceAsync(
            string providerReference,
            CancellationToken cancellationToken = default);
    }
}
