using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Queries.GetPermissionsByModule
{
    public class GetPermissionsByModuleUseCase : IRequestHandler<GetPermissionsByModuleQuery,
                                                IReadOnlyList<PermissionModuleGroupDTO>>
    {
        private readonly IRolesRepository _rolesRepository;

        public GetPermissionsByModuleUseCase(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<IReadOnlyList<PermissionModuleGroupDTO>> Handle(GetPermissionsByModuleQuery request)
        {
            List<Permission> permissions = await _rolesRepository.GetAllPermissionsAsync();

            IReadOnlyList<PermissionModuleGroupDTO> group = permissions
                .GroupBy(p  => p.Module)
                .Select(g => new PermissionModuleGroupDTO
                {   
                    Module = g.Key,

                    Permissions = g.Select(p => new PermissionItemDTO
                    {
                        Id = p.Id,
                        Code = p.Code,
                        Description = p.Description,
                    }).ToList()

                }).ToList();
            
            return group;
          
        }
    }
}
