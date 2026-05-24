using Rental_Project_2026.Application.UseCases.Roles.Queries.GetPermissionsByModule;
using System.ComponentModel.DataAnnotations;

namespace Rental_Project_2026.Web.DTOs.Roles
{
    public class EditRoleDTO : IRolePermissionsForm
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Nombre del rol")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Permisos")]
        public List<Guid> PermissionIds { get; set; } = [];

        public IReadOnlyList<PermissionModuleGroupDTO> PermissionModules { get; set; } = [];
    }
}
