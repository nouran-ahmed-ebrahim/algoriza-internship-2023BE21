using Core.Domain;
using Core.DTO;
using Core.Utilities;
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
        public IActionResult GetAll(int Page, int PageSize, string search);
        #endregion
        public Task<IActionResult> Add(ApplicationUser user, UserRole userRole, bool rememberMe);
        public Task<IActionResult> GetUsersCountInRole(string roleName);
    }
}
