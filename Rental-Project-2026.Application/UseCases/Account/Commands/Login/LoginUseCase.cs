using Rental_Project_2026.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Commands.Login
{
    public class LoginUseCase : IRequestHandler<LoginCommand, AccountSignInResult>
    {
        private readonly IAccountRepository _accountRepository;

        public LoginUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<AccountSignInResult> Handle(LoginCommand request)
        {
            return _accountRepository.SignInAsync(request.UserName, request.Password, request.RememberMe);
        }
    }
}
