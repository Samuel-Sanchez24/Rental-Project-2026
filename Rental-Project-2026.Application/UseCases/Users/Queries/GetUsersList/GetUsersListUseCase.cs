using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList
{
    internal class GetUsersListUseCase : IRequestHandler<GetUsersListQuery, PaginationResponse<UserListItemDTO>>
    {
        private readonly IUsersRepository _userRepository;

        public GetUsersListUseCase(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginationResponse<UserListItemDTO>> Handle(GetUsersListQuery query)
        {
            PaginationResponse<User> pagedUsers = await _userRepository.GetPagedList(
                query.Pagination,
                query.NameFilter,
                query.EmailFilter,
                query.RoleFilter,
                query.StatusFilter);

            List<UserListItemDTO> itemsDTO = pagedUsers.Items
                .Select(u => u.ToDTO())
                .ToList();

            return PaginationResponse<UserListItemDTO>.Create(
                itemsDTO,
                pagedUsers.TotalCount,
                query.Pagination);
        }
    }
}
