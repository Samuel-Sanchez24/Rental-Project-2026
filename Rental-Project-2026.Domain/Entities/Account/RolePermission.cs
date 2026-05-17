using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Domain.Entities.Account
{
    public class RolePermission
    {
        public Guid RoleId { get; private set; }
        public Guid PermissionId { get; private set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }

        private RolePermission() { } 

        public RolePermission(Guid roleId, Guid permissionId)
        {
            if (roleId == Guid.Empty)
            {
                throw new BusinessRulesException("El RoleId no puede ser vacío.");
            }
            if (permissionId == Guid.Empty)
            {
                throw new BusinessRulesException("El PermissionId no puede ser vacío.");
            }

            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}
