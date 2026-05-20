using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Account;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.Update_User
{
    public sealed class UpdateUserUseCase : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUsersRepository _usersRepository;

        public UpdateUserUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task Handle(UpdateUserCommand command)
        {
            User? existing = await _usersRepository.GetByIdAsync(command.Id);

            if (existing is null)
            {
                throw new BusinessRulesException("El usuario no existe.");
            }

            User updated = User.Reconstitute(
                command.Id,
                command.FirstName,
                command.LastName,
                command.Email,
                command.Email,
                existing.EmailConfirmed,
                command.Phone,
                command.Role);

            await _usersRepository.UpdateAsync(updated);
        }

        public Task Handler(UpdateUserCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
