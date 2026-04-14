using Rental_Project_2026.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Commands.ActiveBranch
{
    public class ActivateBranchCommand : IRequest

    {
        public required Guid Id { get; set; }
    }
}
