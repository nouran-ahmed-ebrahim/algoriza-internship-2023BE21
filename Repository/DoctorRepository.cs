using Core.Domain;
using Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DoctorRepository : DataOperationsRepository<Doctor>, IDoctorRepository
    {
        private UserManager<ApplicationUser> _userManager;

        public DoctorRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public int GetDoctorIdByUserId(string UserId)
        {
            Doctor? doctor = _context.Doctors.FirstOrDefault( d => d.DoctorUserId ==  UserId);
            return doctor == null ? 0 : doctor.Id;
        }

        public async Task<ApplicationUser> GetDoctorUser(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<string> GetDoctorIdFromClaim(ApplicationUser user)
        {
            var Claims = await _userManager.GetClaimsAsync(user);
            return Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
