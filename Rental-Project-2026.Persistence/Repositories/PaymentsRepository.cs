using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class PaymentsRepository : Repository<Payment>, IPaymentsRepository
    {
        private readonly DataContext _context;

        public PaymentsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Payment?> GetPaymentByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await BaseQuery()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<Payment?> GetLatestByReservationIdAsync(
            Guid reservationId,
            CancellationToken cancellationToken = default)
        {
            return await BaseQuery()
                .Where(p => p.ReservationId == reservationId)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Payment?> GetPendingByReservationIdAsync(
            Guid reservationId,
            CancellationToken cancellationToken = default)
        {
            return await BaseQuery()
                .Where(p => p.ReservationId == reservationId &&
                            p.Status == PaymentStatus.Pending)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Payment?> GetPaidByReservationIdAsync(
            Guid reservationId,
            CancellationToken cancellationToken = default)
        {
            return await BaseQuery()
                .Where(p => p.ReservationId == reservationId &&
                            p.Status == PaymentStatus.Paid)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Payment?> GetByProviderReferenceAsync(
            string providerReference,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(providerReference))
                return null;

            return await BaseQuery()
                .FirstOrDefaultAsync(
                    p => p.ProviderReference == providerReference,
                    cancellationToken);
        }

        private IQueryable<Payment> BaseQuery()
        {
            return _context.Payments
                .Include(p => p.Reservation)
                    .ThenInclude(r => r.Vehicle)
                .Include(p => p.Reservation)
                    .ThenInclude(r => r.Branch);
        }
    }
}
