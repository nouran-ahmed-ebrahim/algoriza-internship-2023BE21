﻿using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Core.Repository
{
    public interface IApplicationUserRepository: IBaseRepository<ApplicationUser>
    {
        Task<IActionResult> GetUsersCountInRole(string roleName);
        Task<IdentityResult> Add(ApplicationUser user, string roleName, bool rememberMe);
        Task AssignRoleToUser(ApplicationUser user, string roleName);
        Task AddSignInCookie(ApplicationUser user, bool rememberMe);
        Task<bool> IsInRole(ApplicationUser user, string role);
        Task<string> GetUserIdFromClaim(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmail(string Email);
        Task DeleteUser(ApplicationUser user);
        Task SignInUser( ApplicationUser User, bool RememberMe, List<Claim> Claims);
        Task<bool> CheckUserPassword(ApplicationUser user, string password);
    }
}
