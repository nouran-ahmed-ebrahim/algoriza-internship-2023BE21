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
        IEnumerable<ApplicationUser> GetAllAsync(int Page, int PageSize);
        ApplicationUser AddAsync(ApplicationUser entity);
        ApplicationUser UpdateAsync(ApplicationUser entity);
        void DeleteAsync(int id);
        public Task<IActionResult> GetUsersCountInRole(string roleName);
    }
}
