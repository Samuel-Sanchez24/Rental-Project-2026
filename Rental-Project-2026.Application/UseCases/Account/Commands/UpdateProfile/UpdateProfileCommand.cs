using Rental_Project_2026.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest
    {
        public required string UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
