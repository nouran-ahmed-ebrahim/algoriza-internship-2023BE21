using Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("Admin/DiscountCodeCoupon")]
    [ApiController]
    public class AdminSettingController : ControllerBase
    {
        [HttpPost]
        public IActionResult DiscountCodeCoupon(DiscountCodeCoupon DiscountCodeCoupon)
        {

            return Ok();
        }


    }
}
