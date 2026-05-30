using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class ReservationsRepository : Repository<Reservation>, IReservationsRepository
    {
        private readonly DataContext _context;

        public ReservationsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsActiveReservationAsync(
            Guid vehicleId,
            DateTime rentDate,
            DateTime returnDate,
            Guid? excludeReservationId = null,
            CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .AnyAsync(r =>
                    r.VehicleId == vehicleId &&
                    (!excludeReservationId.HasValue || r.Id != excludeReservationId.Value) &&
                    r.Status != ReservationStatus.Cancelled &&
                    r.Status != ReservationStatus.Finished &&
                    rentDate < r.ReturnDate &&
                    returnDate > r.RentDate,
                    cancellationToken);
        }

        public async Task<Reservation?> GetReservationByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .Include(r => r.Vehicle)
                .Include(r => r.Branch)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<PaginationResponse<Reservation>> GetPagedList(
            PaginationRequest request,
            string? userIdFilter,
            Guid? vehicleIdFilter,
            Guid? branchIdFilter,
            ReservationStatus? statusFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Reservation> query = _context.Reservations
                .Include(r => r.Vehicle)
                .Include(r => r.Branch)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(userIdFilter))
                query = query.Where(r => r.UserId == userIdFilter);

            if (vehicleIdFilter.HasValue)
                query = query.Where(r => r.VehicleId == vehicleIdFilter.Value);

            if (branchIdFilter.HasValue)
                query = query.Where(r => r.BranchId == branchIdFilter.Value);

            if (statusFilter.HasValue)
                query = query.Where(r => r.Status == statusFilter.Value);

            int totalCount = await query.CountAsync(cancellationToken);

            List<Reservation> items = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return PaginationResponse<Reservation>.Create(items, totalCount, request);
        }

    }
}
