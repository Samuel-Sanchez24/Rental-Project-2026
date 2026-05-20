using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Queries.GetRolesList
{
    public sealed class GetRolesListUseCase : IRequestHandler<GetRolesListQuery, PaginationResponse<RoleListItemDTO>>
    {
        private readonly IRolesRepository _rolesRepository;

        public GetRolesListUseCase(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<PaginationResponse<RoleListItemDTO>> Handle(GetRolesListQuery query)
        {
            (List<RoleListItemDTO> items, int totalCount) = await _rolesRepository.GetPagedListAsync(
                query.Pagination, query.NameFilter);

            return PaginationResponse<RoleListItemDTO>.Create(items, totalCount, query.Pagination);
        }

    }
}
