using Rental_Project_2026.Application.Contracts.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Queries.GetRolesList
{
    public sealed class GetRolesListQuery : IRequest<PaginationResponse<RoleListItemDTO>>
    {
        public PaginationRequest Pagination { get; init; } = PaginationRequest.Normalized();

        public string? NameFilter { get; init; }
    }
}
