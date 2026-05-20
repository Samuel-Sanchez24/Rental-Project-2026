using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Commands.UpdateRole
{
    public sealed class UpdateRoleCommand : IRequest
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public List<Guid> PermissionIds { get; init; } = [];
    }
}
