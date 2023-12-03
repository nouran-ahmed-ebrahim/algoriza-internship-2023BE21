using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Core.Repository
{
    public interface IApplicationUserRepository: IBaseRepository<ApplicationUser>
    {
        public Task<IActionResult> GetUsersCountInRole(string roleName);
        public Task<IdentityResult> Add(ApplicationUser user, string roleName, bool rememberMe);
        public Task AssignRoleToUser(ApplicationUser user, string roleName);
        public Task AddSignInCookie(ApplicationUser user, bool rememberMe);
        public Task<bool> IsInRole(ApplicationUser user, string role);
        public Task<string> GetUserIdFromClaim(ApplicationUser user);
        public Task<ApplicationUser> GetUserByEmail(string Email);
        public Task DeleteUser(ApplicationUser user);
        public Task<SignInResult> SignInUser(string Email, String Password, bool RememberMe);
    }
}
