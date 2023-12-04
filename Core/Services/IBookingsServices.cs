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
        public Appointment GetAppointment(int appointmentId);

        IActionResult NumOfBookings();
        IActionResult GetAll(int Page, int PageSize, string search);
        IActionResult AddBookingToPatient(string PatientId, int AppointmentTimeId, string DiscountCodeCouponName);
    }
}
