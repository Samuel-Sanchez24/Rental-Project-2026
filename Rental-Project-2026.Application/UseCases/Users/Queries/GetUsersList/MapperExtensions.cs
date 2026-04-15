using Rental_Project_2026.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList
{
    internal static class MapperExtensions
    {
        public static UserListItemDTO ToDTO(this User user)
        {
            return new UserListItemDTO
            {
                id = user.id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                Status = user.Status
            };
        }
    }
}
