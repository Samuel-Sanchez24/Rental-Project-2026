using Rental_Project_2026.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetVehicleById
{
    public class GetVehicleByIdQueryUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public GetVehicleByIdQueryUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<AccessibleVehicleDatailDTO> Handle(GetVehicleByIdQuery request)
        {
            return _accountRepository.GetAccessibleVehicleByIdAsync(
                request.UserId,
                request.VehicleId
            );
        }
    }
}
