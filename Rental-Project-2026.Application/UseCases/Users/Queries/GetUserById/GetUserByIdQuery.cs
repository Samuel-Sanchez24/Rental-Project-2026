using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQuery : IRequest<UserDetailDTO>
    {
        public required string Id { get; init; }
    }
}
