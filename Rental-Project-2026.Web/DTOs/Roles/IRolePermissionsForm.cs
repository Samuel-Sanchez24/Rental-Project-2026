using Rental_Project_2026.Application.UseCases.Roles.Queries.GetPermissionsByModule;

namespace Rental_Project_2026.Web.DTOs.Roles
{
    public interface IRolePermissionsForm
    {
        List<Guid> PermissionIds { get; set; }
        IReadOnlyList<PermissionModuleGroupDTO> PermissionModules { get; set; }

    }
}
