using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IVehiclesRepository : IRepository<Vehicle>
    {
        Task<Vehicle?> GetByPlateAsync(string plate);
        Task<List<Vehicle>> GetByBranchIdAsync(Guid branchId);

        Task<PaginationResponse<Vehicle>> GetPagedList(
            PaginationRequest request,
            string? plateFilter,
            string? modelFilter,
            string? brandFilter,
            string? colorFilter,
            int? yearFilter,
            decimal? priceFilter,
            VehicleStatus? statusFilter,
            CancellationToken cancellationToken);

    }
}
