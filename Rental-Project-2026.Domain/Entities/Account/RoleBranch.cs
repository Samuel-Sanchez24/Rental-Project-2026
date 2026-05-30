using Rental_Project_2026.Domain.Entities.Branches;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using static Rental_Project_2026.Domain.Entities.Account.Role;
using static System.Collections.Specialized.BitVector32;

namespace Rental_Project_2026.Domain.Entities.Account
{
    public class RoleBranch
    {
        public Guid RoleId { get; private set; }
        public Guid PermissionId { get; private set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }

        private RoleBranch() { }

        public RoleBranch(Guid roleId, Guid permissionId)
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
