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
    public interface IDoctorServices : IApplicationUserService
    {
        Task<IActionResult> AddDoctor(UserDTO userDTO, UserRole patient, string specialize);
    }
}
