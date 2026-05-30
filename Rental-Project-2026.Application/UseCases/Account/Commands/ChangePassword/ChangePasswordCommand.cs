using Rental_Project_2026.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest
    {
        public required string UserId { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
