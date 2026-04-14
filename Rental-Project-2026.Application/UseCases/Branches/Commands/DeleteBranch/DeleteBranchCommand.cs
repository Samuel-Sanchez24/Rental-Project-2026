using Rental_Project_2026.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Commands.DeleteBranch
{
    public class DeleteBranchCommand : IRequest
    {
        public required Guid Id { get;  set; } 
    }
}
