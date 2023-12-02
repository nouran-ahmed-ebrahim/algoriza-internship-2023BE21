using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Migrations;
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
        private readonly IMapper _mapper;

        public ApplicationUserService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetUsersCountInRole(string roleName)
        {
            return await _unitOfWork.ApplicationUser.GetUsersCountInRole(roleName);
        }

        #region base methods

        public IActionResult GetAll(int Page, int PageSize, string search)
        {
            return _unitOfWork.ApplicationUser.GetAll(Page, PageSize, search);
        }
        #endregion

        public async Task<IActionResult> Add(UserDTO userDTO, bool rememberMe)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userDTO);

            string? Role = Enum.GetName(UserRole.Patient);

            try
            {
                IdentityResult result = await _unitOfWork.ApplicationUser.Add(user, Role, rememberMe);
                if (result.Succeeded)
                {
                    await _unitOfWork.ApplicationUser.AssignRoleToUser(user, Role);
                    try
                    {
                        await _unitOfWork.ApplicationUser.AddSignInCookie(user, rememberMe);
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.ApplicationUser.deleteUser(user);
                        return new ObjectResult($"An error occurred while Creating cookie \n: {ex.Message}")
                        {
                            StatusCode = 500
                        };
                    }
                    return new OkObjectResult(user);
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.ApplicationUser.deleteUser(user);
                return new BadRequestObjectResult($"{ex.Message}\n {ex.InnerException?.Message}");
            }
        }
    }


}
