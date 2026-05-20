using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList;
using Rental_Project_2026.Domain.Account;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Entities.Account;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IUsersRepository 
    {
        Task<PaginationResponse<UserListItemDTO>> GetPagedListAsync(
            PaginationRequest request,
            string? nameFilter,
            Guid? roleIdFilter,
            CancellationToken cancellationToken = default);

        Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        Task CreateAsync(User user, string password, CancellationToken cancellationToken = default);

        Task UpdateAsync(User user, CancellationToken cancellationToken = default);

        Task DeleteAsync(string id, CancellationToken cancellationToken = default);

        Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken = default);
    }
}
