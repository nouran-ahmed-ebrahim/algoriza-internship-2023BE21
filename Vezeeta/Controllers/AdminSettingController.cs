using Core.Domain;
using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Text.RegularExpressions;

namespace Vezeeta.Controllers
{
    [Route("api/Admin/DiscountCodeCoupon")]
    [ApiController]
    public class AdminSettingController : ControllerBase
    {
        private readonly IDiscountCodeCouponServices _discountCodeCouponServices;
        private readonly IApplicationUserService _adminServices;

        public AdminSettingController(IDiscountCodeCouponServices DiscountCodeCouponServices,
            IApplicationUserService AdminServices) 
        {
            _discountCodeCouponServices = DiscountCodeCouponServices;
            _adminServices = AdminServices;
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

        [HttpGet("SignIn")]
        public async Task<IActionResult> SignIn([FromForm] string Email, [FromForm] string Password, [FromForm] bool RememberMe)
        {
            if (string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("Email", "Email is required");
            }

            if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password", "Password is required");
            }

            string pattern = ".+@.+\\.com";
            bool isMatch = Regex.IsMatch(Email, pattern);

            if (!isMatch)
            {
                ModelState.AddModelError("Email", "Invalid Email");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _adminServices.SignIn(Email, Password, RememberMe);
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _adminServices.SignOut();
            return Ok("LogOut Successfully");
        }
    }
}
