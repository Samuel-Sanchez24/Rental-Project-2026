using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.Update_User
{
    public sealed class UpdateUserCommand : IRequest
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public required Guid Role { get; set; }
    }
}
