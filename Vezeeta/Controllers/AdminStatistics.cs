using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/admin/statistics")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("Doctors")]
        public Task<int> GetNumberOfDoctors()
        {
            return Task.FromResult(0);
        }
    }
}
