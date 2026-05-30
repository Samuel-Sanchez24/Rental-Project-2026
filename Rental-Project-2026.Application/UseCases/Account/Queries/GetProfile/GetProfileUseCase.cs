using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetProfile
{
    public class GetAccountProfileUseCase : IRequestHandler<GetProfileQuery, AccountProfileDTO>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountProfileUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountProfileDTO> Handle(GetProfileQuery query)
        {
            return await _accountRepository.GetProfileAsync(query.UserId);
        }
    }
}
