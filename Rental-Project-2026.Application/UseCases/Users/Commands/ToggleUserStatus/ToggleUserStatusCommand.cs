using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.ToggleUserStatus
{
    public class ToggleUserStatusCommand : IRequest
    {
        public string Id { get; set; } = null!;
    }
}
