using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Account;
using Rental_Project_2026.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.CreateUser
{
    public sealed class CreateUserUseCase : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUsersRepository _usersRepository;

        public CreateUserUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<string> Handle(CreateUserCommand command)
        {
            User user = User.Reconstitute(
                                       id: Guid.CreateVersion7().ToString(),
                                       firstName: command.FirstName,
                                       lastName: command.LastName,
                                       userName: command.Email,
                                       email: command.Email,
                                       emailConfirmed: true,
                                       phone: command.PhoneNumber,
                                       roleId: command.RoleId);

            await _usersRepository.CreateAsync(user, command.Password);

            return user.Id;
        }
    }
}
