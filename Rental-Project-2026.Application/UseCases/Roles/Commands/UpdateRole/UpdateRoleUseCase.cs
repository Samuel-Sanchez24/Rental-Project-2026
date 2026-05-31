using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities.Account;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Roles.Commands.UpdateRole
{
    public sealed class UpdateRoleUseCase : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IRolesRepository _rolesRepository;

        public UpdateRoleUseCase(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task Handler(UpdateRoleCommand command)
        {
            Role? role = await _rolesRepository.GetByIdWithPermissionsAsync(command.Id);

            if (role is null)
            {
                throw new BusinessRulesException("No se encontró el rol.");
            }

            if (await _rolesRepository.ExistsByNameAsync(command.Name, excludeId: command.Id))
            {
                throw new BusinessRulesException("Ya existe un rol con ese nombre.");
            }

            role.UpdateName(command.Name);
            await _rolesRepository.UpdateAsync(role, command.PermissionIds, command.BranchesIds);
        }
    }
}
