using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/Admin/Doctor")]
    [ApiController]
    public class AdminDoctorController : ControllerBase
    {
        private readonly IDoctorServices _doctorService;

        public AdminDoctorController(IDoctorServices DoctorService)
        {
            _doctorService = DoctorService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddDoctor([FromForm] UserDTO userDTO,[FromForm] string specialize)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };

                return await _doctorService.Add(userDTO, UserRole.Patient, userDTO.RememberMe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the patient: {ex.Message}");
            }
        }
    }
}
