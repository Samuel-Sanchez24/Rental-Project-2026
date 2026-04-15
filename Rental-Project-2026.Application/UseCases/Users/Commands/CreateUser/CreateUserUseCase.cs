using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserUseCase : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUsersRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserUseCase(IUsersRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateUserCommand command)
        {
            var existingUser = await _repository.GetByEmailAsync(command.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Ya existe un usuario con este correo.");
            }

            User user = new User(command.Name, command.Email, command.PasswordHash, command.Phone, command.Role);
            try
            {
                User newUser = await _repository.CreateAsync(user);
                await _unitOfWork.CommitAsync();
                return newUser.id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
