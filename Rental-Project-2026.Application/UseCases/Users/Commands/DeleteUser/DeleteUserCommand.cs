using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.DeleteUser
{
    public sealed class DeleteUserCommand : IRequest
    {
        public required string Id { get; set; }
    }
}
