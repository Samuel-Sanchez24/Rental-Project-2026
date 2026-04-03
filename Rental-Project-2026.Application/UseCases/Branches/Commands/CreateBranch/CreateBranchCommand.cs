using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Commands.CreateBranch
{
    public class CreateBranchCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public required BranchStatus Status { get; set; }
    }
}
