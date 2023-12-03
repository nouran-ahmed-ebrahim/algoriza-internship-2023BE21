using Core.Domain;
using Core.Utilities;
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
        IActionResult ChangeBookingState(int BookingId, BookingState bookingState);
        public IActionResult NumOfBookings();
        public IActionResult GetAll(int Page, int PageSize, string search);
        public IActionResult AddBookingToPatient(int AppointmentTimeId, string DiscountCodeCouponName);

    }
}
