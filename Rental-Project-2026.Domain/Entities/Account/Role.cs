using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Domain.Entities.Account
{
    public class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

        private Role() { }
        public Role(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessRulesException("El nombre del rol es requerido.");
            }
            Id = Guid.CreateVersion7();
            Name = name;
        }

        public void UpdateName(string name)
        {
            CheckNameValidation(name);
            Name = name;
        }

        private void CheckNameValidation(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessRulesException("El nombre del rol es requerido.");
            }
            if(name.Length > 64)
            {
                throw new BusinessRulesException("El nombre del rol no puede exceder los 64 caracteres.");
            }
            if (name.Length < 3)
            {
                throw new BusinessRulesException("El nombre del rol no puede tener menos de 3 caracteres.");
            }
        }

        public sealed class Permission
        {
            public Guid Id { get; private set; } 
            public string Name { get; private set; } = string.Empty;

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
