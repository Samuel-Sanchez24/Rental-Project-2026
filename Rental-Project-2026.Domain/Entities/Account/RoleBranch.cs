using Rental_Project_2026.Domain.Entities.Branches;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Domain.Entities.Account
{
    public class RoleBranch
    {
        public Guid RoleId { get; private set; }
        public Guid BranchId { get; private set; }
        public Role Role { get; set; } 
        public Branch Branch { get; set; }

        private RoleBranch() { }

        public RoleBranch(Guid roleId, Guid branchId)
        {
            if (roleId == Guid.Empty)
            {
                throw new BusinessRulesException("El RoleId no puede ser vacío.");
            }
            if (branchId == Guid.Empty)
            {
                throw new BusinessRulesException("El id de la sucursal no puede estar vacío.");
            }

            RoleId = roleId;
            BranchId = branchId;
        }
    }
}
