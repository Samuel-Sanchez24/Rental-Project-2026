using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList
{
    public sealed class GetUsersListQuery : IRequest<PaginationResponse<UserListItemDTO>>
    {
        public PaginationRequest Pagination { get; set; } = PaginationRequest.Normalized();
        public string? NameFilter { get; set; }
        public Guid? RoleIdFilter { get; set; }
    }
}
