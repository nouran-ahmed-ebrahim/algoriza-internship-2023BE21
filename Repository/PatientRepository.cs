using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PatientRepository : ApplicationUserRepository, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager) : 
            base(context, userManager, roleManager, signInManager)
        {
        }

        public async Task<IActionResult> GetPatientsInfo(int Page, int PageSize,  Func<ApplicationUser, bool> criteria = null)
        {
            // Get All patients
            try
            {
                // Get patients
                var patients = (await _userManager.
                         GetUsersInRoleAsync(Enum.GetName(UserRole.Patient))).AsEnumerable();

               
                // Apply criteria - if exists -
                if(criteria != null)
                {
                    patients = patients.Where(criteria);
                }

                // Apply Pagination 
                if (Page != 0)
                    patients = patients.Skip((Page - 1) * PageSize);

                if (PageSize != 0)
                    patients = patients.Take(PageSize);

                // Drop unnecessary columns
                var DesiredPatients = patients.Select(p => new { 
                    p.Image, p.FullName, p.Email, p.Gender, p.PhoneNumber, p.DateOfBirth });

                return new OkObjectResult(DesiredPatients.ToList());
            }
            catch (Exception ex)
            {
                return new ObjectResult($"There is a problem during getting the data {ex.Message}")
                {
                    StatusCode = 500
                };
            }

        }
    }
}
