using Core.Domain;
using Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Migrations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Add(ApplicationUser user, string roleName, bool rememberMe)
        {
            IdentityResult result = await _userManager.CreateAsync(user);
            
            if(result.Succeeded)
            {
                try
                {
                    await AssignRoleToUser(user, roleName);
                    // await AddSignInCookie(user, rememberMe);
                    return new OkObjectResult(user);
                }
                catch (Exception ex)
                {
                    return new NotFoundResult();
                }
            }

            return new BadRequestResult();
        }

        public async Task AddSignInCookie(ApplicationUser user, bool rememberMe)
        {
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
                return new NotFoundResult();
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            int userCount = usersInRole.Count;

            return new OkObjectResult(userCount);
        }


    }
}
