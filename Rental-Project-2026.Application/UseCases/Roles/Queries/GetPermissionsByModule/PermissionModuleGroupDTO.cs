using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Queries.GetPermissionsByModule
{
    public class PermissionModuleGroupDTO
    {
        public required string Module { get; set; }
        public required List<PermissionItemDTO> Permissions { get; set; }
    }
}
