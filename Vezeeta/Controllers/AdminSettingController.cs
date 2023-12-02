using Core.Domain;
using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Vezeeta.Controllers
{
    [Route("api/Admin/DiscountCodeCoupon")]
    [ApiController]
    public class AdminSettingController : ControllerBase
    {
        private readonly IDiscountCodeCouponServices _discountCodeCouponServices;

        public AdminSettingController(IDiscountCodeCouponServices DiscountCodeCouponServices) 
        {
            _discountCodeCouponServices = DiscountCodeCouponServices;
        }

        [HttpPut]
        public IActionResult UpdateDiscountCodeCoupon(DiscountCodeCoupon DiscountCodeCoupon)
        {
            if (DiscountCodeCoupon == null)
            {
                return BadRequest("Inter Coupon");
            }
            //Make Id MAndatory

            if (DiscountCodeCoupon == null || DiscountCodeCoupon.Id == default)
            {
                ModelState.AddModelError("Id", "The Id is required.");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };

                return _discountCodeCouponServices.Update(DiscountCodeCoupon); 

        }

        [HttpPost]
        public IActionResult AddDiscountCodeCoupon(DiscountCodeCoupon DiscountCodeCoupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            return _discountCodeCouponServices.Add(DiscountCodeCoupon);

        }

        [HttpDelete]
        public IActionResult DeleteDiscountCodeCoupon(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            return _discountCodeCouponServices.Delete(id);
        }

        [HttpPatch]
        [Route("Deactivate")]
        public IActionResult DeActivateDiscountCodeCoupon(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            return _discountCodeCouponServices.Deactivate(id);

        }
    }
}
