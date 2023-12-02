using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
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

        public IEnumerable<ApplicationUser> GetAllAsync(int Page, int PageSize)
        {
            return _unitOfWork.ApplicationUser.GetAll(Page, PageSize);
        }
        #endregion

        public Task<IActionResult> Add(UserDTO userDTO, bool rememberMe)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userDTO);

            string? Role = Enum.GetName(UserRole.Patient);

            var result =  _unitOfWork.ApplicationUser.Add(user, Role, rememberMe); 

            _unitOfWork.Complete();
            return result;
        }
    }


}
