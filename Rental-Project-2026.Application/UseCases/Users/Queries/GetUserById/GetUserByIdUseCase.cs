using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Account;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUserById
{
    public sealed class GetUserByIdUseCase : IRequestHandler<GetUserByIdQuery, UserDetailDTO>
    {
        private readonly IUsersRepository _usersRepository;

        public GetUserByIdUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserDetailDTO> Handle(GetUserByIdQuery query)
        {
            User? user = await _usersRepository.GetByIdAsync(query.Id);

            if (user is null)
            {
                throw new BusinessRulesException("El usuario no existe.");
            }

            return new UserDetailDTO
            {
                Id = user.Id,
                FirstName = user.FisrtName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
            };
        }
    }
}
