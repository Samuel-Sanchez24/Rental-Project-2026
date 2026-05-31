using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Queries.GetRoleById
{
    public class RoleDetailDTO
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required List<Guid> PermissionIds { get; init; }
        public required List<Guid> BranchesIds { get; init; }
    }
}
