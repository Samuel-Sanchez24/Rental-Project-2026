using Rental_Project_2026.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Commands.Logout
{
    internal class LogoutUseCase : IRequestHandler<LogoutCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public LogoutUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handler(LogoutCommand request)
        {
            await _accountRepository.SignOutAsync();
        }
    }
}
