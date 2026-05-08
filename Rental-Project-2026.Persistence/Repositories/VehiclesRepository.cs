using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Persistence.Extensions;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class VehiclesRepository : Repository<Vehicle>, IVehiclesRepository
    {
        private readonly DataContext _context;
        public VehiclesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Vehicle?> GetByPlateAsync(string plate)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(v => v.Plate == plate);
        }

        public async Task<List<Vehicle>> GetByBranchIdAsync(Guid branchId)
        {
            return await _context.Vehicles.Where(v => v.BranchId == branchId).ToListAsync();
        }

        public async Task<PaginationResponse<Vehicle>> GetPagedList(
            PaginationRequest request,
            string? plateFilter,
            string? modelFilter,
            string? brandFilter,
            string? colorFilter,
            int? yearFilter,
            decimal? priceFilter,
            VehicleStatus? statusFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Vehicle> query = _context.Vehicles.AsQueryable();
            if (!string.IsNullOrWhiteSpace(plateFilter))
            {
                string term = plateFilter.Trim();
                query = query.Where(v => v.Plate.Contains(term));
            }
            if (!string.IsNullOrWhiteSpace(modelFilter))
            {
                string term = modelFilter.Trim();
                query = query.Where(v => v.Model.Contains(term));
            }
            if (!string.IsNullOrWhiteSpace(brandFilter))
            {
                string term = brandFilter.Trim();
                query = query.Where(v => v.Brand.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(colorFilter))
            {
                string term = colorFilter.Trim();
                query = query.Where(v => v.Color.Contains(term));
            }
            if (yearFilter.HasValue)
                query = query.Where(v => v.Year == yearFilter.Value);
            if (priceFilter.HasValue)
                query = query.Where(v => v.DailyPrice == priceFilter.Value);
            if (statusFilter.HasValue)
            {
                query = query.Where(v => v.Status == statusFilter);
            }
            
            query = query.OrderBy(v => v.Plate);    
            return await query.ToPagedListAsync(request, cancellationToken);
        }

    }
}
