using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Commands.ChangePassword
{
    public class ChangePasswordUseCase : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public ChangePasswordUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handler(ChangePasswordCommand command)
        {
            await _accountRepository.ChangePasswordAsync(
                command.UserId,
                command.CurrentPassword,
                command.NewPassword);
        }
    }
}
