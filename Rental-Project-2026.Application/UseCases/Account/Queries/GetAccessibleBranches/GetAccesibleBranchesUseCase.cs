using Rental_Project_2026.Application.Contracts.Repositories;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetAccessibleBranches
{
    public class GetAccessibleBranchesUseCase
        : IRequestHandler<GetAccessibleBranchesQuery, IReadOnlyList<AccessibleBranchItemDTO>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccessibleBranchesUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<IReadOnlyList<AccessibleBranchItemDTO>> Handle(GetAccessibleBranchesQuery request)
        {
            return _accountRepository.GetAccessibleBranchesAsync(request.UserId);
        }
    }
}
