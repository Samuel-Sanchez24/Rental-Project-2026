using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.UseCases.Users.Commands.CreateUser;   

namespace Rental_Project_2026.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
    }
}
