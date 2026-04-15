using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Domain.Entities
{
    public class User
    {
        public Guid id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }

        public User(string name, string email, string passwordHash, string phone, UserRole role)
        {
            ApplyBusinessRules(name, email, passwordHash, phone, role);

            id = Guid.CreateVersion7();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Phone = phone;
            Role = role;
            Status = UserStatus.Active;
        }

        public void UpdateUser(string name, string email, string phone, UserRole role)
        {
            ApplyBusinessRules(name, email, phone, role);
            Name = name;
            Email = email; 
            Phone = phone;
            Role = role;
        }

        public void Activate() => Status = UserStatus.Active;
        public void Deactivate() => Status = UserStatus.Inactive;

        public void ApplyBusinessRules(string name, string email, string passwordHash, string phone, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2 || name.Length > 50)
                throw new BusinessRulesException($"El {nameof(name)} es requerido (2-50 caracteres).");
            if (string.IsNullOrWhiteSpace(email) || email.Length < 5 || email.Length > 100 || !email.Contains("@"))
                throw new BusinessRulesException($"El {nameof(email)} es requerido y debe ser un correo válido (5-100 caracteres).");
            if (string.IsNullOrWhiteSpace(passwordHash) || passwordHash.Length < 8)
                throw new BusinessRulesException($"El {nameof(passwordHash)} es requerido y debe tener al menos 8 caracteres.");
            if (string.IsNullOrWhiteSpace(phone) || phone.Length < 7 || phone.Length > 12)
                throw new BusinessRulesException($"El {nameof(phone)} debe tener entre 7 y 12 dígitos.");
            if (!Enum.IsDefined(typeof(UserRole), role))
                throw new BusinessRulesException($"El {nameof(role)} no es válido.");
        }

        //SOBRECARGA PARA UPDATE (SIN PASSWORD)
        public void ApplyBusinessRules(string name, string email, string phone, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2 || name.Length > 50)
                throw new BusinessRulesException($"El {nameof(name)} es requerido (2-50 caracteres).");
            if (string.IsNullOrWhiteSpace(email) || email.Length < 5 || email.Length > 100 || !email.Contains("@"))
                throw new BusinessRulesException($"El {nameof(email)} es requerido y debe ser un correo válido (5-100 caracteres).");
            if (string.IsNullOrWhiteSpace(phone) || phone.Length < 7 || phone.Length > 12)
                throw new BusinessRulesException($"El {nameof(phone)} debe tener entre 7 y 12 dígitos.");
            if (!Enum.IsDefined(typeof(UserRole), role))
                throw new BusinessRulesException($"El {nameof(role)} no es válido.");
        }
    }
}
