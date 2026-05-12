using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IUsersRepository 
    {
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetListAsync();

        Task<PaginationResponse<User>> GetPagedList(
            PaginationRequest request,
            string? nameFilter,
            string? emailFilter,
            UserRole? roleFilter,
            UserStatus? statusFilter,
            CancellationToken cancellationToken = default);
    }
}
