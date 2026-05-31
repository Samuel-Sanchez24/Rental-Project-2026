using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities.Account;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Queries.GetRoleById
{
    public class GetRoleByIdUseCase : IRequestHandler<GetRoleByIdQuery, RoleDetailDTO>
    {
        private readonly IRolesRepository _rolesRepository;

        public GetRoleByIdUseCase(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<RoleDetailDTO> Handle(GetRoleByIdQuery query)
        {
            Role? role = await _rolesRepository.GetByIdWithPermissionsAsync(query.Id);

            if( role == null)
            {
                throw new BusinessRulesException("El rol no existe");
            }
            return new RoleDetailDTO
            {
                Id = role.Id,
                Name = role.Name,
                PermissionIds = role.RolePermissions.Select(rp => rp.PermissionId).ToList(),

                BranchesIds = role.RoleBranches.Select(rs => rs.BranchId).ToList(),
            };
        }
    }
}
