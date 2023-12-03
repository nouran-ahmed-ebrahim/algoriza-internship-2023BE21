﻿using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using Services;


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

        [HttpGet]
        public async Task<IActionResult> SignIn(string Email, string Password, bool RememberMe)
        {
            if(string.IsNullOrEmpty(Email))
            {
                return BadRequest("Email is required");
            }
            
            if (string.IsNullOrEmpty(Password))
            {
                return BadRequest("Password is required");
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
}
}
