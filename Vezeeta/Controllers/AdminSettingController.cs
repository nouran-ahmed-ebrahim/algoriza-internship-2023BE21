using Core.Domain;
using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Vezeeta.Controllers
{
    [Route("Admin/DiscountCodeCoupon")]
    [ApiController]
    public class AdminSettingController : ControllerBase
    {
        private readonly IDiscountCodeCouponServices _discountCodeCouponServices;

        public AdminSettingController(IDiscountCodeCouponServices DiscountCodeCouponServices) 
        {
            _discountCodeCouponServices = DiscountCodeCouponServices;
        }

        [HttpPost]
        public IActionResult UpdateDiscountCodeCoupon(DiscountCodeCoupon DiscountCodeCoupon)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };

                return _discountCodeCouponServices.Update(DiscountCodeCoupon); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the patient: {ex.Message}");
            }
        }


    }
}
