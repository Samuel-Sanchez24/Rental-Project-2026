using Rental_Project_2026.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Commands.DeactivateBranch
{
    public class DeactivateBranchCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
