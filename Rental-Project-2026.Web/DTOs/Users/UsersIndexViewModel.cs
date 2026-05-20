using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetRoleOptions;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Web.DTOs.Users
{
    public class UsersIndexViewModel
    {
        public required PaginationResponse<UserListItemDTO> List { get; set; }
        public string FilterName { get; set; } = string.Empty;
        public Guid? FilterRoleId { get; set; }
        public IReadOnlyList<RoleOptionDTO> Roles { get; set; } = [];
    }
}
