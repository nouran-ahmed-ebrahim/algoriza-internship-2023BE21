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

        public IActionResult Add(DiscountCodeCoupon entity)
        {
            throw new NotImplementedException();
        }

        public IActionResult Deactivate(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IActionResult Update(DiscountCodeCoupon entity)
        {
            var result = _unitOfWork.DiscountCodeCoupons.Update(entity);

            _unitOfWork.Complete();
            return result;
        }
    }
}
