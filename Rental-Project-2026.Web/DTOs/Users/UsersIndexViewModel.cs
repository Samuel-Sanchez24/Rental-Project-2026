using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Web.DTOs.Users
{
    public class UsersIndexViewModel
    {
        public required PaginationResponse<UserListItemDTO> List { get; init; }
        public string FilterName { get; init; } = string.Empty;
        public string FilterEmail { get; init; } = string.Empty;
        public UserRole? FilterRole { get; init; }
        public UserStatus? FilterStatus { get; init; }
    }
}
