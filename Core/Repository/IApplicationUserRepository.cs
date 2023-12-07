using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Core.Repository
{
    public interface IApplicationUserRepository: IBaseRepository<ApplicationUser>
    {
        Task<IActionResult> GetUsersCountInRole(string roleName);
        //Task<IActionResult> Add(ApplicationUser user);
        public string GetFullName(string id);

        #region Authentiation
        Task AssignRoleToUser(ApplicationUser user, string roleName);
        Task AddSignInCookie(ApplicationUser user, bool rememberMe);
        Task<bool> IsInRole(ApplicationUser user, string role);
        Task SignInUser(ApplicationUser User, bool RememberMe, List<Claim> Claims);
        Task SignOut();
        #endregion

        Task<ApplicationUser> GetUserByEmail(string Email);
        Task<bool> CheckUserPassword(ApplicationUser user, string password);
        Task<ApplicationUser> GetUser(string UserId);

        Task DeleteUser(ApplicationUser user);
        Task<IActionResult> Update(ApplicationUser user);
    }
}
