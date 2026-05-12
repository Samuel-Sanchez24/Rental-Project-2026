using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDetailDTO>
    {
        public string Id { get;}
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
