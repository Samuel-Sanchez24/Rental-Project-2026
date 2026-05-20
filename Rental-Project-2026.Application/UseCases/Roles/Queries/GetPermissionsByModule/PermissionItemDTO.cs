using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Queries.GetPermissionsByModule
{
    public class PermissionItemDTO
    {
        public required Guid Id { get; init; }
        public required string Code { get; init; }
        public required string Description { get; init; }
    }
}
