using Microsoft.AspNetCore.Identity;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.UseCases.Account.Commands.Login;
using Rental_Project_2026.Persistence.Entities;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<AccountSignInResult> SignInAsync(string email, string password, bool rememberMe, CancellationToken cancellationToken = default)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if(user is null)
            {
                return new AccountSignInResult
                {
                    Succeeded = false,
                    IsLockedOut = false,
                };
            }

            SignInResult  result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: true);

            return new AccountSignInResult
            {
                Succeeded = result.Succeeded,
                IsLockedOut = result.IsLockedOut,
            };
        }

        public Task SignOutAsync(CancellationToken cancellationToken = default)
        {
            return _signInManager.SignOutAsync();
        }
    }
}
