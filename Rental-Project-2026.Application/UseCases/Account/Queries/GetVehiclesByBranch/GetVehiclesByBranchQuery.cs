namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetVehiclesByBranch
{
    public class GetVehiclesByBranchQuery : IRequest<AccessibleBranchVehiclesDTO>
    {
        public required string UserId { get; init; }
        public Guid BranchId { get; init; }
    }
}