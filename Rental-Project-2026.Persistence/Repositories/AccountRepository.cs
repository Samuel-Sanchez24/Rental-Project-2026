using Microsoft.AspNetCore.Identity;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.UseCases.Account.Commands.Login;
using Rental_Project_2026.Application.UseCases.Account.Queries.GetAccountUserInfo;
using Rental_Project_2026.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataContext _context;

        public AccountRepository(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<UserAccountInfoDTO> GetUserInfoAsync(string UserId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return null!;
            }

            ApplicationUser? user = await _context.Users.Include(u => u.Role)
                                                        .FirstOrDefaultAsync(u => u.Id == UserId);

            if (user == null)
            {
                return null!;
            }

            return new UserAccountInfoDTO
            {
                FirsName = user.Firtsname,
                LastName = user.Lastname,
                RoleName = user.Role.Name,
            };
        }

        public Task<bool> HasPermissionAsync(string userId, string permissionCode)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountSignInResult> SignInAsync(string email, string password, bool rememberMe, CancellationToken cancellationToken = default)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new AccountSignInResult
                {
                    Succeeded = false,
                    IsLockedOut = false,
                };
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: true);

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

        public async Task<bool> UserHasPermissionAsync(string userId, string permissionCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(permissionCode))
            {
                return false;
            }

            ApplicationUser? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                return false;
            }

            return await _context.Permissions.AnyAsync(p => p.Code == permissionCode
                && p.RolePermissions.Any(rp => rp.RoleId == user.RoleId));
        }
    }
}
