using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using Services;
using System.Text.RegularExpressions;


namespace Vezeeta.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatientServices _patientServices;
        private readonly IBookingsServices _bookingsServices;
        public PatientController(IPatientServices PatientServices, 
            IBookingsServices bookingsServices) {
            _patientServices = PatientServices;
            _bookingsServices = bookingsServices;
        }

        [HttpGet("SignIn")]
        public async Task<IActionResult> SignIn(string Email, string Password, bool RememberMe)
        {
            if(string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("Email", "Email is required");
            }

            if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password","Password is required");
            }

            string pattern = ".+@.+\\.com";
            bool isMatch = Regex.IsMatch(Email, pattern);

            if (!isMatch)
            {
                ModelState.AddModelError("Email", "Invalid Email");
            }

            if (! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _patientServices.SignIn(Email, Password, RememberMe);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddPatient([FromForm]UserDTO userDTO) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };

                return await _patientServices.AddUser(userDTO,UserRole.Patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the patient: {ex.Message}");
            }
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _patientServices.SignOut();
            return Ok("LogOut Successfully");
        }
    }
}

//{
//  "firstName": "nouran",
//  "lastName": "ahmed",
//  "email": "nourana245@gmail.com",
//  "password": "nourana245@",
//  "phone": "0123456",
//  "gender": 1,
//  "dateOfBirth": "2001-05-08",
//  "rememberMe": true
//}