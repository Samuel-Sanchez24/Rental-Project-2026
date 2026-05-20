using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<RoleDetailDTO>
    {
        public required Guid Id { get; init; }  
    }
}
