using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IApplicationUserService
    {
        #region Base methods
        IEnumerable<ApplicationUser> GetAllAsync(int Page, int PageSize);
        ApplicationUser UpdateAsync(ApplicationUser entity);
        void DeleteAsync(int id);
        #endregion
        public Task<IActionResult> Add(ApplicationUser user, string roleName, bool rememberMe);
        public Task<IActionResult> GetUsersCountInRole(string roleName);
    }
}
