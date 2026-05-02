using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Entities.Branches;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IBranchesRepository : IRepository<Branch>
    {
        Task<Branch?> GetByCityAsync(string city);
        Task<PaginationResponse<Branch>> GetPagedList(
            PaginationRequest request,
            string? nameFilter,
            string? cityFilter,
            string? addressFilter,
            string? phoneFilter,
            BranchStatus? statusFilter,
            CancellationToken cancellationToken = default);
    }
}
