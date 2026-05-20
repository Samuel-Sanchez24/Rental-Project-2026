using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities.Account;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Roles.Commands.DeleteRole
{
    internal class DeleteRoleUseCase : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRolesRepository _roleRepository;

        public DeleteRoleUseCase(IRolesRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task Handler(DeleteRoleCommand command)
        {
            Role? role = await _roleRepository.GetByIdWithPermissionsAsync(command.Id);

            if (role == null)
            {
                throw new BusinessRulesException("El rol no existe.");
            }

            if (await _roleRepository.HasUserAsync(command.Id))
            {
                throw new BusinessRulesException("No se puede eliminar el rol porque hay usuarios asociados a él.");
            }

            await _roleRepository.DeleteAsync(command.Id);
        }
    }
}
