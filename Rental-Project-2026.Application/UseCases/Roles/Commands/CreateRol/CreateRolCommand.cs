using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Commands.CreateRol
{
    public sealed class CreateRolCommand : IRequest<Guid>
    {
        public required string Name { get; init; }
        public List<Guid> PermissionIds { get; init; } = [];
        public List<Guid> BranchesIds { get; init; } = [];
    }
}
