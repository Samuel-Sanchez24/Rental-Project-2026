using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Domain.Entities.Account
{
    public class Role
    {
        public Role(string roleName)
        {
            RoleName = roleName;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
        public string RoleName { get; }

        public sealed class Permission
        {
            public Guid Id { get; private set; }
            public string Name { get; private set; }

            public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

            private Permission() { }

            public Permission(string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new BusinessRulesException("El nombre del rol es requerido.");
                }

                Id = Guid.CreateVersion7();
                Name = name;
            }
        }
    }
}
