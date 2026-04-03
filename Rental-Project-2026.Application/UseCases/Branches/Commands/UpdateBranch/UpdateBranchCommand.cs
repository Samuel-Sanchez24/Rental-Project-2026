namespace Rental_Project_2026.Application.UseCases.Branches.Commands.UpdateBranch
{
    public class UpdateBranchCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required string City { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public required BranchStatus Status { get; set; }
    }
}
