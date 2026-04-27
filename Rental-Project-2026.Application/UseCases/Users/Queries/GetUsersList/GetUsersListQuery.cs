using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<PaginationResponse<UserListItemDTO>>
    {
        public PaginationRequest Pagination { get; init; } = PaginationRequest.Normalized();
        public string? NameFilter { get; init; }
        public string? EmailFilter { get; init; }
        public UserRole? RoleFilter { get; init; }
        public UserStatus? StatusFilter { get; init; }
    }
}
