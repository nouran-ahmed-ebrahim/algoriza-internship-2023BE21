﻿using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingsRepository : CommonRepository<Booking>, IBookingsRepository
    {
        public BookingsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public int NumOfBookings(Expression<Func<Booking, bool>> criteria)
        {
            return _context.Bookings.Count(criteria);
        }

        public int NumOfBooKings()
        {
            return _context.Bookings.Count();
        }

        public IActionResult GetPatientBookings(string PatientId)
        {
            try
            {
                var bookings = _context.Bookings.Where(b => b.PatientId == PatientId);

                // get Appointment info
                var bookingsWithAppointment = bookings.Join(
                                     _context.AppointmentTimes,
                                     b => b.AppointmentTimeId,
                                     at => at.Id,
                                     (b, at) => new
                                     {
                                         b.DoctorId,
                                         b.BookingState,
                                         b.DiscountCodeCouponId,
                                         at.AppointmentId,
                                         Time = at.Time.ToString(),
                                     }
                                    ).Join
                                    (
                                        _context.Appointments,
                                        b => b.AppointmentId,
                                        a => a.Id,
                                        (b,a) => new
                                        {
                                            b.DoctorId,
                                            b.BookingState,
                                            b.DiscountCodeCouponId,
                                            b.Time,
                                            day = a.DayOfWeek.ToString() 
                                        }
                                    );

                // get coupon info -left join-

                var bookingsWithCouponInfo = bookingsWithAppointment.GroupJoin(
                                              _context.DiscountCodeCoupons,
                                              booking => booking.DiscountCodeCouponId,
                                              coupon => coupon.Id,
                                              (booking,coupon) => new
                                              {
                                                 booking,
                                                 coupon
                                              }).SelectMany(
                                                  coupon => coupon.coupon.DefaultIfEmpty() ,
                                                  (b,c) => new
                                                  {
                                                      b.booking.DoctorId,
                                                      b.booking.BookingState,
                                                      b.booking.day,
                                                      b.booking.Time,
                                                      DiscountType = (c == null)? 0: c.DiscountType,
                                                      Value = (c == null)? 0 : c.Value,
                                                      Name = (c == null) ? "No Coupon": c.Name
                                                  }
                                              );
                // get doctor info
                var bookingsWithDoctorsInfo = bookingsWithCouponInfo.Join(
                                                _context.Doctors,
                                                b => b.DoctorId,
                                                d => d.Id,
                                                (b, d) => new
                                                {
                                                    d.Price,
                                                    d.DoctorUserId,
                                                    d.SpecializationId,
                                                    b.BookingState,
                                                    b.day,
                                                    b.Time,
                                                    b.DiscountType,
                                                    b.Value,
                                                    b.Name
                                                }
                                              ).Join(
                                                _context.Users,
                                                b => b.DoctorUserId,
                                                u => u.Id,
                                                (b, u) => new
                                                {
                                                    u.FullName,
                                                    b.Price,
                                                    u.Image,
                                                    b.SpecializationId,
                                                    BookingState =b.BookingState.ToString(),
                                                    b.day,
                                                    b.Time,
                                                    b.DiscountType,
                                                    b.Value,
                                                    b.Name
                                                }
                                              ).Join(
                                                _context.Specializations,
                                                b => b.SpecializationId,
                                                s => s.Id,
                                                (b, s) => new BookingDTO
                                                {
                                                   price = b.Price.Value,
                                                   DoctorName = b.FullName,
                                                   ImagePath = b.Image,
                                                   Specialization = s.Name,
                                                   BookingStatus = b.BookingState,
                                                   Day = b.day,
                                                   Time = b.Time,
                                                   DiscountType = b.DiscountType,
                                                   CouponValue = b.Value,
                                                   discoundCodeName = b.Name
                                                }
                                              ).ToList();

                return new OkObjectResult(bookingsWithDoctorsInfo);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while Getting Patient's Bookings \n: {ex.Message}")
                {
                    StatusCode = 500
                };
            }
        }
    }
}
