using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList;

namespace Rental_Project_2026.Web.DTOs.Branches
{
    public class BranchesIndexViewModel
    {
        public required PaginationResponse<BranchListItemDTO> List { get; init; }
        public string FilterName { get; init; } = string.Empty;
        public string FilterCity { get; init; } = string.Empty;
        public BranchStatus? FilterStatus { get; init; }
    }
}
