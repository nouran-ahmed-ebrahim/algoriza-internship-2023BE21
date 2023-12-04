using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
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

        #region authentication APIs
        [HttpPost("SignIn")]
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

        #endregion

        #region Appointments APIs
        [HttpPost("Appointments")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddAppointments(AppointmentsDTO appointments)
        {
            if (appointments == null)
            {
                return BadRequest("Price and appointment are required");

            }

            if (appointments.Price <= 0)
            {
               return BadRequest("Invalid Price");
            }

            if(appointments.days == null)
            {
                return BadRequest("Appointments is required");
            }

            string? doctorId = (User.Claims.FirstOrDefault(c => c.Type == "DoctorId")?.Value);

            if (int.TryParse(doctorId, out int id))
            {
                return _doctorServices.AddAppointments(id, appointments);
            }
            else
            {
               return new ObjectResult("There is a problem in current user data\n Invalid DoctorId")
                {
                    StatusCode = 500
                };
            }
        }
        #endregion

        #region Booking APIs
        [HttpPatch("Booking/Confirm")]
        [Authorize(Roles = "Doctor")]
        public IActionResult ConfirmBooking([FromForm]int BookingId)
        {
            return _doctorServices.ConfirmCheckUp(BookingId);
        }
        #endregion
    }
}
