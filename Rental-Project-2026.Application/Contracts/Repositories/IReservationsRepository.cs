using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IReservationsRepository : IRepository<Reservation>
    {
        Task<bool> ExistsActiveReservationAsync(
            Guid vehicleId,
            DateTime rentDate,
            DateTime returnDate,
            CancellationToken cancellationToken = default);

        Task<Reservation?> GetReservationByIdAsync(
            Guid Id,
            CancellationToken cancellationToken = default);

        Task<PaginationResponse<Reservation>> GetPagedList(
            PaginationRequest request,
            string? userIdFilter,
            Guid? vehicleIdFilter,
            Guid? branchIdFilter,
            ReservationStatus? statusFilter,
            CancellationToken cancellationToken = default);
    }
}
