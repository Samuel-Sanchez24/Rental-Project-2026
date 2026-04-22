using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.ToggleUserStatus
{
    public class ToggleUserStatusUseCase : IRequestHandler<ToggleUserStatusCommand>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleUserStatusUseCase(IUsersRepository usersRepository, IUnitOfWork unitOfWork)
        {
            _usersRepository = usersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handler(ToggleUserStatusCommand command)
        {
            User? user = await _usersRepository.GetByIdAsync(command.id);

            if (user == null)
                throw new BusinessRulesException("El usuario no existe.");

            if (user.Status == UserStatus.Active)
                user.Deactivate();
            else
                user.Activate();

            await _usersRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
        }
    }
}
