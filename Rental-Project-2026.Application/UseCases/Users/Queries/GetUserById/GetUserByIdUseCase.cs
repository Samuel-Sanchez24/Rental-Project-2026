using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUserById
{
    internal class GetUserByIdUseCase : IRequestHandler<GetUserByIdQuery, UserDetailDTO>
    {
        private readonly IUsersRepository _usersRepository;

        public GetUserByIdUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
        public async Task<UserDetailDTO> Handle(GetUserByIdQuery request)
        {
            User? user = await _usersRepository.GetByIdAsync(request.id);
            if (user == null)
            {
                throw new BusinessRulesException("El usuario no existe.");
            }
            return new UserDetailDTO
            {
                id = user.id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                Status = user.Status
            };
        }
    }
}
