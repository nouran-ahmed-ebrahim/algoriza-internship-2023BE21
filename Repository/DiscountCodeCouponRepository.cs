using Core.Domain;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DiscountCodeCouponRepository : DataOperationsRepository<DiscountCodeCoupon>, IDiscountCodeCouponRepository
    {
        public DiscountCodeCouponRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IActionResult Deactivate(int id)
        {
            try
            {
                DiscountCodeCoupon DiscountCodeCoupon = GetById(id);
                if (DiscountCodeCoupon != null)
                {
                    return new NotFoundObjectResult($"DiscountCodeCoupon with {id} is not found");
                }

                DiscountCodeCoupon.IsActivated = false;
                Update(DiscountCodeCoupon);
                return new OkObjectResult(DiscountCodeCoupon);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while Deactivating the coupone \n: {ex.Message}")
                {
                    StatusCode = 500
                };
            }

        }
    }
}
