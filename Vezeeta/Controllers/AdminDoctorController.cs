using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Migrations;
using System.Reflection.Metadata.Ecma335;

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
        public async Task<IActionResult> AddDoctor([FromForm] UserDTO userDTO, [FromForm] string Specialize)
        {
            try
            {
                if (string.IsNullOrEmpty(Specialize))
                {
                    ModelState.AddModelError("Specialize", "Specialize Is Required");
                }
                if (userDTO.Image == null || userDTO.Image.Length == 0)
                {
                    ModelState.AddModelError("userDTO.Image", "Image Is Required");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };

                return  await _doctorService.AddDoctor(userDTO, UserRole.Patient, Specialize);

                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the Doctor: {ex.Message}");
            }
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateDoctor([FromForm] UserDTO userDTO, [FromForm] string Specialize)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDoctor([FromForm]int id)
        {
            try
            {
                if (id == 0)
                {
                    ModelState.AddModelError("id", "id Is Required");
                }
                else if (id < 0)
                {
                    ModelState.AddModelError("id", "id Is Invalid. Id must be greater than 0");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };
                return await _doctorService.Delete(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while Deleting the Doctor:\n" +
                    $"  {ex.Message} \n  {ex.Message}");
            }
        }       
    }
}
