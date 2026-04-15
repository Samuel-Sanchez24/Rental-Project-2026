using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.Update_User
{
    public class UpdateUserUseCase : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserUseCase(IUsersRepository usersRepository, IUnitOfWork unitOfWork)
        {
            _usersRepository = usersRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handler(UpdateUserCommand command)
        {
            User? user = await _usersRepository.GetByIdAsync(command.id);
            if (user == null)
            {
                throw new BusinessRulesException("El usuario no existe.");
            }

            var existingUserWithEmail = await _usersRepository.GetByEmailAsync(command.Email);
            if (existingUserWithEmail != null && existingUserWithEmail.id != command.id)
                throw new BusinessRulesException("El correo electrónico ya está en uso por otro usuario.");

            user.UpdateUser(command.Name, command.Email, command.Phone, command.Role);
            if (command.Status == UserStatus.Active)
            {
                user.Activate();
            }
            else
            {
                user.Deactivate();
            }

            try
            {
                await _usersRepository.UpdateAsync(user);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }


        }
    }
}
