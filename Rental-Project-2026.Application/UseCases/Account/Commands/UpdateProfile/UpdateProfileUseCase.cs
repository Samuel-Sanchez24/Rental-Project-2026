using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Commands.UpdateProfile
{
    public class UpdateProfileUseCase : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateProfileUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handler(UpdateProfileCommand request)
        {
            await _accountRepository.UpdateProfileAsync(
                request.UserId,
                request.FirstName,
                request.LastName,
                request.PhoneNumber);
        }
    }
}
