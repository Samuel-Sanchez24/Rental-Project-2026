using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.UserHasPermission
{
    public class UserHasPermissionQuery : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string PermissionCode{ get; set; }
    }
}
