using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Commands.DeleteRole
{
    public sealed class DeleteRoleCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
