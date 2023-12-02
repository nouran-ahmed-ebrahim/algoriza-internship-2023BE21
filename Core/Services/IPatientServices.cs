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
    public interface IPatientServices:IApplicationUserService
    {
        public Task<IActionResult> Add(PatientDTO userDTO, UserRole userRole, bool rememberMe);

    }
}
