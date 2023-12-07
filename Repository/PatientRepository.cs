using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public bool IsExist(string id)
        {
            string PatientRoleName = UserRole.Patient.ToString();
            string PatientRoleId = _context.Roles.
                                        Where(r => r.Name == PatientRoleName)
                                        .Select(r => r.Id).SingleOrDefault().ToString();

            return _context.UserRoles.Any(x => x.UserId == id && x.RoleId == PatientRoleId);
        }
        public async Task<IActionResult> GetAllPatients(int Page, int PageSize, Func<PatientDTO, bool> criteria = null)
        {
            // Get All patients
            try
            {
                // Get patients
                var patients = (await _userManager.
                         GetUsersInRoleAsync(Enum.GetName(UserRole.Patient))).AsEnumerable();

                // Drop unnecessary columns
                IEnumerable<PatientDTO> DesiredPatients = patients.Select(p => new PatientDTO
                {
                    ImagePath = p.Image,
                    FullName = p.FullName,
                    Email = p.Email,
                    Phone = p.PhoneNumber,
                    Gender = p.Gender.ToString(),
                    DateOfBirth = p.DateOfBirth.ToString()
                });

                // Apply criteria - if exists -
                if (criteria != null)
                {
                    DesiredPatients = DesiredPatients.Where(criteria);
                }

                // Apply Pagination 
                if (Page != 0)
                    DesiredPatients = DesiredPatients.Skip((Page - 1) * PageSize);

                if (PageSize != 0)
                    DesiredPatients = DesiredPatients.Take(PageSize);

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
