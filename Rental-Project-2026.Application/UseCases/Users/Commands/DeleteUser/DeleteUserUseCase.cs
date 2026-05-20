using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Account;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.DeleteUser
{
    public sealed class DeleteUserUseCase : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUsersRepository _usersRepository;

        public DeleteUserUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task Handler(DeleteUserCommand request)
        {
            User? user = await _usersRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                throw new BusinessRulesException("El usuario no existe.");  
            }

            await _usersRepository.DeleteAsync(request.Id);
        }
    }
}
