﻿using Core.Domain;
using Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Repository
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Add(ApplicationUser user, string roleName, bool rememberMe)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task AddSignInCookie(ApplicationUser user, bool rememberMe)
        {
            /// await  _signInManager.PasswordSignInAsync(user.Email, user.PasswordHash, rememberMe, false);
            
            await _signInManager.SignInAsync(user, rememberMe);
        }

        public async Task AssignRoleToUser(ApplicationUser user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IActionResult> GetUsersCountInRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return new NotFoundObjectResult($"There is no Role called {roleName}");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            int userCount = usersInRole.Count;

            return new OkObjectResult(userCount);
        }

        public async Task DeleteUser(ApplicationUser user)
        {
           await _userManager.DeleteAsync(user);
        }

        public async Task SignInUser(ApplicationUser User, bool RememberMe, List<Claim> Claims)
        {
             await _signInManager.SignInWithClaimsAsync(User, RememberMe, Claims);
        }

        public Task<bool> IsInRole(ApplicationUser user, string role)
        {
            return _userManager.IsInRoleAsync(user, role);
        }

        public async Task<ApplicationUser> GetUserByEmail(string Email)
        {
            return await _userManager.FindByEmailAsync(Email);
        }

        public async Task<string> GetUserIdFromClaim(ApplicationUser user)
        {
            var Claims = await _userManager.GetClaimsAsync(user);
            return Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        public async Task<bool> CheckUserPassword(ApplicationUser user, string password)
        {
           return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
