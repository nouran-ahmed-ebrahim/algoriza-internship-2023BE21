using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDiscountCodeCouponServices
    {
        IActionResult Add(DiscountCodeCoupon entity);
        IActionResult Update(DiscountCodeCoupon entity);
        IActionResult Delete(int Id);
        IActionResult Deactivate(int id);
    }
}
