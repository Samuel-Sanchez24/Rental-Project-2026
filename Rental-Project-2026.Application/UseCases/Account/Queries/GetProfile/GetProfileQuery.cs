using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetProfile
{
    public class GetProfileQuery : IRequest<AccountProfileDTO>
    {
        public required string UserId { get; set; }

    }
}
