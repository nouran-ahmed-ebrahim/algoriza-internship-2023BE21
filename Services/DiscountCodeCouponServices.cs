using Core.Domain;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DiscountCodeCouponServices : IDiscountCodeCouponServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscountCodeCouponServices(IUnitOfWork UnitOfWork) {
            _unitOfWork = UnitOfWork;
        }

        public IActionResult Add(DiscountCodeCoupon Coupon)
        {
            try
            {
                var result = _unitOfWork.DiscountCodeCoupons.Add(Coupon);

                _unitOfWork.Complete();
                return result;
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult($"{ex.Message} \n {ex.InnerException?.Message}");
            }
        }

        public IActionResult Deactivate(int id)
        {
            try
            {
                DiscountCodeCoupon DiscountCodeCoupon = _unitOfWork.DiscountCodeCoupons.GetById(id);
                if (DiscountCodeCoupon == null)
                {
                    return new NotFoundObjectResult($"DiscountCodeCoupon with {id} is not found");
                }

                DiscountCodeCoupon.IsActivated = false;
                var result = _unitOfWork.DiscountCodeCoupons.Update(DiscountCodeCoupon);

                _unitOfWork.Complete();
                return result;
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult($"{ex.Message} \n {ex.InnerException?.Message}");
            }
        }

        public IActionResult Delete(int Id)
        {
            try
            {
                var IsExist = _unitOfWork.DiscountCodeCoupons.IsExist(Id);
                if (IsExist is not OkObjectResult okResult)
                {
                    return IsExist;
                }

                DiscountCodeCoupon coupon = okResult.Value as DiscountCodeCoupon;
                var result = _unitOfWork.DiscountCodeCoupons.Delete(coupon);
                _unitOfWork.Complete();
                return result;
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult($"{ex.Message} \n {ex.InnerException?.Message}");
            }
        }

        public IActionResult Update(DiscountCodeCoupon Coupon)
        {
            try
            {
                var result = _unitOfWork.DiscountCodeCoupons.Update(Coupon);

                _unitOfWork.Complete();
                return result;
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult($"{ex.Message} \n {ex.InnerException?.Message}");
            }
        }
    }
}
