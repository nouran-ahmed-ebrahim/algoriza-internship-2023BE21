using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Vezeeta.Controllers
{
    [Route("api/Admin/Patient")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminPatientController : ControllerBase
    {
        private readonly IPatientServices _patientService;

        public AdminPatientController(IPatientServices PatientService)
        {
            _patientService = PatientService;
        }


        [HttpGet("~/api/Admin/Patients")]
        public async Task<IActionResult> GetAll([FromForm] int page, [FromForm] int pageSize, [FromForm] string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _patientService.GetAllPatients(page, pageSize, search);
        }

    }
}
