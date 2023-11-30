using Core.Domain;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.ApplicationUserService;

namespace Services
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public async Task<IActionResult> GetUsersCountInRole(string roleName)
        {
            return await _unitOfWork.ApplicationUser.GetUsersCountInRole(roleName);
        }

        public void DeleteAsync(int id)
        {
             _unitOfWork.ApplicationUser.Delete(id);
        }

        public IEnumerable<ApplicationUser> GetAllAsync(int Page, int PageSize)
        {
            return _unitOfWork.ApplicationUser.GetAllAsync(Page, PageSize);
        }

        public ApplicationUser UpdateAsync(ApplicationUser entity)
        {
            return _unitOfWork.ApplicationUser.Update(entity);
        }

        public Task<IActionResult> Add(ApplicationUser user, string roleName, bool rememberMe)
        {
            return _unitOfWork.ApplicationUser.Add(user, roleName, rememberMe); 
        }
    }


}
