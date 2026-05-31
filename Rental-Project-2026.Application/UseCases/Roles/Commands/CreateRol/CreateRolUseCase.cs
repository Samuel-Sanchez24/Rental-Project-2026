using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities.Account;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Roles.Commands.CreateRol
{
    public sealed class CreateRolUseCase : IRequestHandler<CreateRolCommand, Guid>
    {
        private readonly IRolesRepository _rolesRepository;

        public CreateRolUseCase(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<Guid> Handle(CreateRolCommand command)
        {
            if (await _rolesRepository.ExistsByNameAsync(command.Name))
            {
                throw new BusinessRulesException("Ya existe un rol con ese nombre.");
            }

            Role role = new Role(command.Name);

            await _rolesRepository.CreateAsync(role, command.PermissionIds, command.BranchesIds);

            return role.Id;
        }
    }
}
