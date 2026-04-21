using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
