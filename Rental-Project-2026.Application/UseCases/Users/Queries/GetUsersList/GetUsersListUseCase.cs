using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Application.Contracts.Repositories;

namespace Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList
{
    internal class GetUsersListUseCase : IRequestHandler<GetUsersListQuery, IEnumerable<UserListItemDTO>>
    {
        private readonly IUsersRepository _userRepository;

        public GetUsersListUseCase(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserListItemDTO>> Handle(GetUsersListQuery request)
        {
            IEnumerable<User> users = await _userRepository.GetListAsync();

            List<UserListItemDTO> userDTO = users.Select(u => u.ToDTO()).ToList();
            return userDTO;

        }
    }
}
