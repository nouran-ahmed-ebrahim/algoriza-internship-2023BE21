using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PatientServices(IUnitOfWork UnitOfWork, IMapper mapper) : ApplicationUserService(UnitOfWork, mapper), IPatientServices
    {
        public Task<IActionResult> Add(PatientDTO userDTO, UserRole userRole, bool rememberMe)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userDTO);
            return Add(user, userRole, rememberMe);
        }
    }
}
