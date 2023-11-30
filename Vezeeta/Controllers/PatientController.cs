using Core.Domain;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Vezeeta.Controllers
{
    [Route("api/PatientController")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IBookingsServices _bookingsServices;
        public PatientController(IApplicationUserService ApplicationUserService, IBookingsServices bookingsServices) {
            _applicationUserService = ApplicationUserService;
            _bookingsServices = bookingsServices;
        }
        [HttpPost("Patient")]
        public async Task<IActionResult> AddPatient([FromBody]ApplicationUser user) 
        {
            //Image part
            // revert f,lasr name =>fullname
            // create userDTO Takes remeber me, first,lat, passwored
            // map(pass => hass, full name) pass rmem
            // Validate the input data
            // confermed pass
            // act phone
            // email val
            // requierd from User table
            // remove route
            //  ore readable state code
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return await _applicationUserService.Add(user, "Patient", false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the patient: {ex.Message}");
            }
        }
    }
}
