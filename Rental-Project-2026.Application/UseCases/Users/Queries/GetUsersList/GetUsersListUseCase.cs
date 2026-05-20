using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList
{
    public class GetUsersListUseCase : IRequestHandler<GetUsersListQuery, PaginationResponse<UserListItemDTO>>
    {
        private readonly IUsersRepository _usersRepository;

        public GetUsersListUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<PaginationResponse<UserListItemDTO>> Handle(GetUsersListQuery query)
        {
            return await _usersRepository.GetPagedListAsync(
                query.Pagination,
                query.NameFilter,
                query.RoleIdFilter);
        }
    }
}
