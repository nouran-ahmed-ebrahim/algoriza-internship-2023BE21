using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IBookingsServices
    {
        public IActionResult NumOfBookings();
        IEnumerable<Booking> GetAllAsync(int Page, int PageSize);

    }
}
