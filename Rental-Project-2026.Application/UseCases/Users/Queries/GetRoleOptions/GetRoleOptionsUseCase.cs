using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetRoleOptions;
using Rental_Project_2026.Domain.Entities.Account;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetRoleOptions
{
    public sealed class GetRoleOptionsUseCase : IRequestHandler<GetRoleOptionsQuery, IReadOnlyList<RoleOptionDTO>>
    {
        private readonly IUsersRepository _usersRepository;

        public GetRoleOptionsUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<IReadOnlyList<RoleOptionDTO>> Handle(GetRoleOptionsQuery query)
        {
            List<Role> roles = await _usersRepository.GetRolesAsync();

            return roles.Select(r => new RoleOptionDTO
            {
                Id = r.Id,
                Name = r.Name,
            }).ToList();
        }
    }
}
