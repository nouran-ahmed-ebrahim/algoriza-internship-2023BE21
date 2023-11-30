using Core.Domain;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ApplicationUserService(IApplicationUserRepository userRepository)
        {
            _applicationUserRepository = userRepository;
        }

        public async Task<IActionResult> GetUsersCountInRole(string roleName)
        {
            return await _applicationUserRepository.GetUsersCountInRole(roleName);
        }
    }


}
