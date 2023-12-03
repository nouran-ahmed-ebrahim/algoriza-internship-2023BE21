using Core.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IDoctorRepository : IDataOperationsRepository<Doctor>
    {
        int GetDoctorIdByUserId(string UserId);
        Task<ApplicationUser> GetDoctorUser(string userId);
        Task<string> GetDoctorIdFromClaim(ApplicationUser user);

    }
}
