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
        private readonly IBookingsServices _bookingsServices;

        public DiscountCodeCouponServices(IUnitOfWork UnitOfWork, IBookingsServices BookingsServices) {
            _unitOfWork = UnitOfWork;
            _bookingsServices = BookingsServices;
        }

        public async Task<IActionResult> Add(DiscountCodeCoupon Coupon)
        {
            try
            {
                Coupon.Id = 0;
                var result = await _unitOfWork.DiscountCodeCoupons.Add(Coupon);

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
                DiscountCodeCoupon coupon = _unitOfWork.DiscountCodeCoupons.GetById(id);
                if (coupon == null)
                {
                    return new NotFoundObjectResult($"Id {id} is not found");
                }

                coupon.IsActivated = false;
                var result = _unitOfWork.DiscountCodeCoupons.Update(coupon);

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
                DiscountCodeCoupon coupon = _unitOfWork.DiscountCodeCoupons.GetById(Id);
                if (coupon == null)
                {
                    return new NotFoundObjectResult($"Id {Id} is not found");
                }

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
                bool IsCouponExist = _unitOfWork.DiscountCodeCoupons.IsExist(c => c.Id == Coupon.Id);
                if (!IsCouponExist)
                {
                    return new NotFoundObjectResult($"Id {Coupon.Id} is not found");
                }
                var result = _unitOfWork.DiscountCodeCoupons.Update(Coupon);

                _unitOfWork.Complete();
                return result;
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult($"{ex.Message} \n {ex.InnerException?.Message}");
            }
        }

        public IActionResult CheckCouponApplicability(DiscountCodeCoupon discountCodeCoupon, string patientId)
        {
            // Check if is active
            if (!discountCodeCoupon.IsActivated)
            {
                return new BadRequestObjectResult($"DiscountCodeCoupon {discountCodeCoupon.Name}" +
                    $" is deactivated");
            }

            // check minimum booking
            bool IsMeet = _bookingsServices.CheckMinimumRequests(patientId,
                discountCodeCoupon.MinimumRequiredRequests);

            if (!IsMeet)
            {
                return new BadRequestObjectResult($"You must have atleast " +
                    $"{discountCodeCoupon.MinimumRequiredRequests} to use {discountCodeCoupon.Name}" +
                    $" coupon");
            }

            // Check if is used 
            bool IsUsed = _bookingsServices.CheckMinimumRequests(patientId,
                discountCodeCoupon.MinimumRequiredRequests);

            if (IsUsed)
            {
                return new BadRequestObjectResult($"You have already used this coupon");
            }

            return new OkResult();
        }
    }
}
