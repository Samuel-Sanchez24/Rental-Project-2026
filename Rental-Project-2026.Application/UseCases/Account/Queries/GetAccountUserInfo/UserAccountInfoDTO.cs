using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetAccountUserInfo
{
    public class UserAccountInfoDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string RoleName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

    }
}
