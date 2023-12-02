using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Services
{
    public class ApplicationUserService: IApplicationUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

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

        public async Task<IActionResult> AddUser(UserDTO userDTO, UserRole userRole)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userDTO);
            string? Role = Enum.GetName(userRole);

            try
            {
                IdentityResult result = await _unitOfWork.ApplicationUser.Add(user, Role, userDTO.RememberMe);
                if (result.Succeeded)
                {
                    await _unitOfWork.ApplicationUser.AssignRoleToUser(user, Role);
                    try
                    {
                        await _unitOfWork.ApplicationUser.AddSignInCookie(user, userDTO.RememberMe);
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
