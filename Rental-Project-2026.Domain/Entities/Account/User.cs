using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rental_Project_2026.Domain.Account
{
    public class User
    {

        public string Id { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? Phone { get; set; }
        public Guid RoleId { get; set; }

        private User()
        {
        }

        public static User Reconstitute(string id,
                                        string firstName,
                                        string lastName,
                                        string userName,
                                        string email,
                                        bool emailConfirmed,
                                        string? phone,
                                        Guid roleId)
        {
            ValideteId(id);
            ValideteNames(firstName, lastName);
            ValideteEmail(email);
            ValideteRoleId(roleId);

            return new User
            {
                Id = id,
                FisrtName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                EmailConfirmed = emailConfirmed,
                Phone = phone,
                RoleId = roleId,
            };
        }

        private static void ValideteId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new BusinessRulesException("El id es requerido");
            }
        }

        private static void ValideteRoleId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BusinessRulesException("El rol es requerido");
            }
        }

        private static void ValideteNames(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new BusinessRulesException("El nombre es requerido");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new BusinessRulesException("El apellido es requerido");
            }

            if (firstName.Length > 64)
            {
                throw new BusinessRulesException("El nombre no puede tener más de 64 caracteres");
            }

            if (lastName.Length > 64)
            {
                throw new BusinessRulesException("El apellido no puede tener más de 64 caracteres");
            }
        }

        private static void ValideteEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new BusinessRulesException("El correo electrónico es requerido");
            }

            // TODO: Validar el formato del correo electrónico
        }

    }
}
