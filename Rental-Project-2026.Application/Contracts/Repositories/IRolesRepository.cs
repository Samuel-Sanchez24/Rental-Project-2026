using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Roles.Queries.GetRolesList;
using Rental_Project_2026.Domain.Account;
using Rental_Project_2026.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IRolesRepository 
    {   
        Task<(List<RoleListItemDTO> Items, int TotalCount)> GetPagedListAsync(
            PaginationRequest request,
            string? nameFilter,
            CancellationToken cancellationToken = default);

        Task<Role?> GetByIdWithPermissionsAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);

        Task CreateAsync(Role role, List<Guid> permissionIds, List<Guid> branchIds, CancellationToken cancellationToken = default);

        Task UpdateAsync(Role role, List<Guid> permissionIds, List<Guid> branchIds, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> HasUserAsync(Guid roleId, CancellationToken cancellationToken = default);

        Task<List<Permission>> GetAllPermissionsAsync(CancellationToken cancellationToken = default);

    }
}
