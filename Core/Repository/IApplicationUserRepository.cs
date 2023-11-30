using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IApplicationUserRepository: IBaseRepository<ApplicationUser>
    {
        public Task<IActionResult> GetUsersCountInRole(string roleName);
        public Task<IActionResult>Add(ApplicationUser user, string roleName, bool rememberMe);
        public Task AssignRoleToUser(ApplicationUser user, string roleName);
        public Task AddSignInCookie(ApplicationUser user, bool rememberMe);
    }
}
