using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<PaginationResponse<User>> GetPagedList(
            PaginationRequest request,
            string? nameFilter,
            string? emailFilter,
            UserRole? roleFilter,
            UserStatus? statusFilter,
            CancellationToken cancellationToken = default);
    }
}
