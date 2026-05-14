using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.Contracts.Repositories;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.UserHasPermission
{
    public class UserHasPermissionUseCase : IRequestHandler<UserHasPermissionQuery, bool>
    {
        private readonly IAccountRepository _accountRepository;

        public UserHasPermissionUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<bool> Handle(UserHasPermissionQuery request)
        {
            return _accountRepository.UserHasPermissionAsync(request.UserId, request.PermissionCode);
        }
    }
}
