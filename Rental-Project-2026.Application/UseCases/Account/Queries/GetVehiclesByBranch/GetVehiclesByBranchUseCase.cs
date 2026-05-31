using Rental_Project_2026.Application.Contracts.Repositories;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetVehiclesByBranch
{
    public class GetVehiclesByBranchUseCase
        : IRequestHandler<GetVehiclesByBranchQuery, AccessibleBranchVehiclesDTO>
    {
        private readonly IAccountRepository _accountRepository;

        public GetVehiclesByBranchUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<AccessibleBranchVehiclesDTO> Handle(GetVehiclesByBranchQuery request)
        {
            return _accountRepository.GetAccessibleVehicleByBranchAsync(
                request.UserId,
                request.BranchId
            );
        }
    }
}