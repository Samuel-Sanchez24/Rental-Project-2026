using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Roles.Queries.GetRolesList;

namespace Rental_Project_2026.Web.DTOs.Roles
{
    public class RolesIndexViewModel
    {
        public required PaginationResponse<RoleListItemDTO> List { get; set; }
        public string FilterName { get; set; } = string.Empty;
    }
}
