using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Domain.Entities.Account
{
    public sealed class Permission
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public String Module { get; private set; } = string.Empty;

        public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

        private Permission() { }

        public Permission(string code, string description, string module)
        {
            if (string.IsNullOrWhiteSpace(code))

                throw new BusinessRulesException("El codigo es requerido");

            if (string.IsNullOrWhiteSpace(description))

                throw new BusinessRulesException("La description es requerida");

            if (string.IsNullOrWhiteSpace(module))

                throw new BusinessRulesException("El modulo es requerido");

            Id = Guid.CreateVersion7();
            Code = code;
            Description = description;
            Module = module;
        }
    }
}
