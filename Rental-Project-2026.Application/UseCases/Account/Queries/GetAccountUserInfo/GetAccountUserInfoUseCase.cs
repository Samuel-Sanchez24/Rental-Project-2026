using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.Contracts.Repositories;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetAccountUserInfo
{
    public class GetAccountUserInfoUseCase : IRequestHandler<GetAccountUserInfoQuery, UserAccountInfoDTO>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountUserInfoUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<UserAccountInfoDTO> Handle(GetAccountUserInfoQuery request)
        {
            return _accountRepository.GetUserInfoAsync(request.UserId.ToString());
        }

    }
}
