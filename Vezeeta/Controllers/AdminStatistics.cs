using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;

namespace Vezeeta.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/admin/statistics")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IBookingsServices _bookingsServices;

        public ValuesController(IUnitOfWork unitOfWork, IBookingsServices bookingsServices) {
            _unitOfWork = unitOfWork;
            _bookingsServices = bookingsServices;
        }
        
        [HttpGet("Doctors")]
        public IActionResult GetNumberOfDoctors()
        {
            return Ok(0);
        }

        [HttpGet("Patients")]
        public IActionResult GetNumberOfPatients()
        {
            return Ok(0);
        }

        [HttpGet("Bookings")]
        public IActionResult GetNumberOfBookings()
        {
            return Ok(_bookingsServices.NumOfBookings().Result);
        }
    }
}
