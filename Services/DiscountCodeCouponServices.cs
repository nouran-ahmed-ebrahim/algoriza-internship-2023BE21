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
            var result = _unitOfWork.DiscountCodeCoupons.Add(Coupon);

            _unitOfWork.Complete();
            return result;
        }

        public IActionResult Deactivate(int id)
        {
            var result = _unitOfWork.DiscountCodeCoupons.Deactivate(id);

            _unitOfWork.Complete();
            return result;
        }

        public IActionResult Delete(int Id)
        {
            return _unitOfWork.DiscountCodeCoupons.Delete(Id);
        }

        public IActionResult Update(DiscountCodeCoupon Coupon)
        {
            var result = _unitOfWork.DiscountCodeCoupons.Update(Coupon);

            _unitOfWork.Complete();
            return result;
        }
    }
}
