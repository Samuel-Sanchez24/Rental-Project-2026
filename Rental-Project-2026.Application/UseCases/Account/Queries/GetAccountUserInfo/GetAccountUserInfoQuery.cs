using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetAccountUserInfo
{
    public class GetAccountUserInfoQuery : IRequest<UserAccountInfoDTO>
    {
        public Guid UserId { get; set; }
    }
}
