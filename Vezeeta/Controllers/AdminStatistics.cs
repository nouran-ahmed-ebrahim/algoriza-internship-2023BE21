using Core.Domain;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Migrations;
using Services;

namespace Vezeeta.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/admin/statistics")]
    [ApiController]
    public class AdminStatistics : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IBookingsServices _bookingsServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminStatistics(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
                            IApplicationUserService ApplicationUserService, IBookingsServices bookingsServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationUserService = ApplicationUserService;
            _bookingsServices = bookingsServices;
        }
        
        [HttpGet("Doctors")]
        public IActionResult GetNumberOfDoctors()
        {
            return _applicationUserService.GetUsersCountInRole("Doctors").Result;
        }

        [HttpGet("Patients")]
        public IActionResult GetNumberOfPatients()
        {
            return _applicationUserService.GetUsersCountInRole("Patient").Result;
        }

        [HttpGet("Bookings")]
        public IActionResult GetNumberOfBookings()
        {
            return  _bookingsServices.NumOfBookings();
        }
    }
}
