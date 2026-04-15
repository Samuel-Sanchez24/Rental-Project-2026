using Rental_Project_2026.Application.Contracts.Repositories;

namespace Rental_Project_2026.Application.UseCases.Users.Commands.DeleteUser
{
    internal class DeleteUserUseCase : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserUseCase(IUsersRepository usersRepository, IUnitOfWork unitOfWork)
        {
            _usersRepository = usersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handler(DeleteUserCommand command)
        {
            var user = await _usersRepository.GetByIdAsync(command.id);
            if (user == null)
            {
                throw new BusinessRulesException($"El usuario con id: {command.id} no existe");
            }
            user.Deactivate();

            await _usersRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
        }
    }
}
