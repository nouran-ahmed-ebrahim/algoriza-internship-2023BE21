using Core.Domain;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Vezeeta.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IBookingsServices _bookingsServices;
        public PatientController(IApplicationUserService ApplicationUserService, IBookingsServices bookingsServices) {
            _applicationUserService = ApplicationUserService;
            _bookingsServices = bookingsServices;
        }
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody]ApplicationUser user) 
        {
            //Image part
            // revert f,lasr name =>fullname
            // create userDTO Takes remeber me, first,lat, passwored
            // map(pass => hass, full name) pass rmem
            // Validate the input data:
            //   act phone
            //   email val
            //   requierd from User table
            //more readable state code
            // dont forget add add admin
            // dont fforget uncoment cookie
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string? Role = Enum.GetName(UserRole.Admin);
                return await _applicationUserService.Add(user, Role, false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the patient: {ex.Message}");
            }
        }

  //      {
  //"userName": "stwwwreeeesssing",
  //"email": "www",
  //"phoneNumber": "striwwng",
  //"firstName": "ww",
  //"lastName": "dd",
  //"gender": 0,
  //"dateOfBirth": "2023-11-30"}
}
}
