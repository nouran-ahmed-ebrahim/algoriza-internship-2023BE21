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
        private readonly IPatientServices _doctorServices;

        public DoctorController(IPatientServices DoctorServices) 
        {
            _doctorServices = DoctorServices;
        }

        [HttpGet("SignIn")]
        public async Task<IActionResult> SignIn(string Email, string Password, bool RememberMe)
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
    }
}
