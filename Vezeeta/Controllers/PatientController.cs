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


namespace Vezeeta.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IBookingsServices _bookingsServices;
        public PatientController(IApplicationUserService ApplicationUserService, 
            IBookingsServices bookingsServices, IMapper mapper) {
            _mapper = mapper;
            _applicationUserService = ApplicationUserService;
            _bookingsServices = bookingsServices;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddPatient([FromForm]UserDTO userDTO) 
        {

            // don't forget uncomment cookie
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };


                ApplicationUser user = _mapper.Map<ApplicationUser>(userDTO);
                string? Role = Enum.GetName(UserRole.Patient);
                return await _applicationUserService.Add(user, Role, false);// userDTO.RememberMe);
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
