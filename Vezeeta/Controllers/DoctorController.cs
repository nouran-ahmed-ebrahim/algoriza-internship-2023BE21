using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Text.RegularExpressions;

namespace Vezeeta.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices;

        public DoctorController(IDoctorServices DoctorServices) 
        {
            _doctorServices = DoctorServices;
        }

        [HttpGet("SignIn")]
        public async Task<IActionResult> SignIn([FromForm]string Email, [FromForm] string Password, [FromForm] bool RememberMe)
        {
            if (string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("Email", "Email is required");
            }

            if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password", "Password is required");
            }

            string pattern = ".+@.+\\.com";
            bool isMatch = Regex.IsMatch(Email, pattern);

            if (!isMatch)
            {
                ModelState.AddModelError("Email", "Invalid Email");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _doctorServices.SignIn(Email, Password, RememberMe);
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _doctorServices.SignOut();
            return Ok("LogOut Successfully");
        }

        [HttpPost("Appointments")]
        public async Task<IActionResult> AddAppointments([FromForm] int Price,
            [FromForm] Dictionary<DayOfWeek, List<TimeSpan>> Appointments)
        {
            if(Price <= 0)
            {
                ModelState.AddModelError("Price","Invalid Price");
            }

            if(Appointments == null)
            {
                ModelState.AddModelError("Appointments", "Appointments is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? doctorId = (User.Claims.FirstOrDefault(c => c.Type == "DoctorId")?.Value);

            if (int.TryParse(doctorId, out int id))
            {
                return _doctorServices.AddAppointments(id,Price, Appointments);
            }
            else
            {
               return new ObjectResult("There is a problem in current user data\n Invalid DoctorId")
                {
                    StatusCode = 500
                };
            }
        }
    }
}
