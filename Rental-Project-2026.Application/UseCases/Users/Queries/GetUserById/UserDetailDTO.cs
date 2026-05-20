using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUserById
{
    public class UserDetailDTO
    {
        public required string Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public required string? Phone { get; init; }
        public required Guid RoleId { get; init; }
    }
}
