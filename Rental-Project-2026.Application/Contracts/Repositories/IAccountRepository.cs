using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.UseCases.Account.Commands.Login;
using Rental_Project_2026.Application.UseCases.Account.Queries.GetAccountUserInfo;

namespace Rental_Project_2026.Application.Contracts.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountSignInResult> SignInAsync(string userName, string password, bool rememberMe,CancellationToken cancellationToken = default);
        
        Task SignOutAsync(CancellationToken cancellationToken = default);

        Task<UserAccountInfoDTO> GetUserInfoAsync(string UserId, CancellationToken cancellationToken = default);
        
        Task<bool> UserHasPermissionAsync(String userId,string permissionCode, CancellationToken cancellationToken = default);
        Task<bool> HasPermissionAsync(string userId, string permissionCode);
    }
}
