using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;  
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string Phone { get; set; } = null!;
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        
        
        private User() { }

        public User(
            string firstName,
            string lastName,
            string userName,
            string email,
            string phone,
            UserRole role)
        {
            ValidateNames(firstName, lastName);
            ValidateUserName(userName);
            ValidateEmail(email);
            ValidatePhone(phone);
            ValidateRole(role);

            Id = Guid.CreateVersion7().ToString();
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            EmailConfirmed = false;
            Phone = phone;
            Role = role;
            Status = UserStatus.Active;
        }


        public static User Reconstitute(
            string id,
            string firstName,
            string lastName,
            string userName,
            string email,
            bool emailConfirmed,
            string phone,
            UserRole role,
            UserStatus status)
        {
            ValidateId(id);
            ValidateNames(firstName, lastName);
            ValidateUserName(userName);
            ValidateEmail(email);
            ValidatePhone(phone);

            return new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                EmailConfirmed = emailConfirmed,
                Phone = phone,
                Role = role,
                Status = status
            };
        }

        private static void ValidateId(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                throw new BusinessRulesException("El Id del usuario es requerido.");
            }
        }

        private static void ValidateNames(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 50)
                throw new BusinessRulesException($"El {nameof(firstName)} es requerido (2-50 caracteres).");
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 50)
                throw new BusinessRulesException($"El {nameof(lastName)} es requerido (2-50 caracteres).");
        }

        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || email.Length < 5 || email.Length > 80 || !email.Contains("@"))
                throw new BusinessRulesException($"El {nameof(email)} es requerido y debe ser un correo válido (5-80 caracteres).");
        }

        private static void ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.Length < 3 || userName.Length > 20)
                throw new BusinessRulesException($"El {nameof(userName)} es requerido (3-20 caracteres).");
        }

        private static void ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone) || phone.Length < 7 || phone.Length > 12)
                throw new BusinessRulesException($"El {nameof(phone)} debe tener entre 7 y 12 dígitos.");
        }

        private static void ValidateRole(UserRole role)
        {
            if (!Enum.IsDefined(typeof(UserRole), role))
                throw new BusinessRulesException($"El {nameof(role)} no es válido.");
        }

        public void UpdateUser(
            string firstName,
            string lastName,
            string userName,
            string email,
            string phone,
            UserRole role)
        {
            ValidateNames(firstName, lastName);
            ValidateUserName(userName);
            ValidateEmail(email);
            ValidatePhone(phone);
            ValidateRole(role);

            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Phone = phone;
            Role = role;
        }

        public void Activate() => Status = UserStatus.Active;
        public void Deactivate() => Status = UserStatus.Inactive;

    }
}
