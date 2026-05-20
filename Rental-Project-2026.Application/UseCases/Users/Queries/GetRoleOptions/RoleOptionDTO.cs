using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetRoleOptions
{
    public class RoleOptionDTO
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
