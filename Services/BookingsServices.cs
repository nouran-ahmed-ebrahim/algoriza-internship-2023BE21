﻿using Core.Domain;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Services
{
    public class BookingsServices : IBookingsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingsServices(IUnitOfWork UnitOfWork,
            IAppointmentServices AppointmentServices) {
            _unitOfWork = UnitOfWork;
        }
        public IActionResult NumOfBookings()
        {

            int totalBookings = _unitOfWork.Bookings.NumOfBooKings();
            int pendingBookings = _unitOfWork.Bookings.NumOfBookings((Booking b) => b.BookingState == BookingState.Pending);
            int completedBookings = _unitOfWork.Bookings.NumOfBookings((Booking b) => b.BookingState == BookingState.Completed);
            int cancelledBookings = _unitOfWork.Bookings.NumOfBookings((Booking b) => b.BookingState == BookingState.Cancelled);

            var result = new
            {
                TotalBookings = totalBookings,
                PendingBookings = pendingBookings,
                CompletedBookings = completedBookings,
                CancelledBookings = cancelledBookings
            };
            
            return new OkObjectResult(result);
        }

        public IActionResult AddBookingToPatient(string PatientId, int AppointmentTimeId, string DiscountCodeCouponName)
        {

            // check if Appointment empty
            bool IsAvailable = CheckAppointmentAvailability(AppointmentTimeId);
            if(!IsAvailable)
            {
                return new BadRequestObjectResult("AppointmentTime is held by another patient");
            }

            // Add Booking
            Booking NewBooking = new()
            {
                PatientId = PatientId,
                AppointmentTimeId = AppointmentTimeId,
            };

            if (!string.IsNullOrEmpty(DiscountCodeCouponName))
            {
                // Get DiscountCoupon
                DiscountCodeCoupon discountCodeCoupon = _unitOfWork.DiscountCodeCoupons.GetByName(DiscountCodeCouponName);

                // Check if it applicable
                var ValiditionReult = CheckCouponApplicability(discountCodeCoupon, PatientId);
                if (ValiditionReult is not OkResult)
                {
                    return ValiditionReult;
                }
                NewBooking.DiscountCodeCouponId = discountCodeCoupon.Id;
            }

            try
            {
                _unitOfWork.Bookings.Add(NewBooking);
                _unitOfWork.Complete();

            }
            catch (Exception ex)
            {
                new ObjectResult($"There is a Problem during booking Appointment \n" +
                    $"{ex.Message} \n {ex.InnerException?.Message}")
                {
                    StatusCode = 500,
                };
            }
            return new OkObjectResult(NewBooking);
        }

        private bool CheckAppointmentAvailability(int appointmentTimeId)
        {
            bool IsHeld = _unitOfWork.Bookings.IsExist(a => a.AppointmentTimeId == appointmentTimeId &&
             a.BookingState == BookingState.Pending);

            return !IsHeld;
        }
        private IActionResult CheckCouponApplicability(DiscountCodeCoupon discountCodeCoupon, string patientId)
        {
            // Check if is active
            if (!discountCodeCoupon.IsActivated)
            {
                return new BadRequestObjectResult($"DiscountCodeCoupon {discountCodeCoupon.Name}" +
                    $" is deactivated");
            }

            // check minimum booking
            bool IsMeet = CheckMinimumBookings(patientId,
                discountCodeCoupon.MinimumRequiredBookings);

            if (!IsMeet)
            {
                return new BadRequestObjectResult($"You must have atleast " +
                    $"{discountCodeCoupon.MinimumRequiredBookings} to use {discountCodeCoupon.Name}" +
                    $" coupon");
            }

            // Check if is used 
            bool IsUsed = CheckIfCouponUsedPreviously(patientId, discountCodeCoupon.Id);

            if (IsUsed)
            {
                return new BadRequestObjectResult($"You have already used this coupon");
            }

            return new OkResult();
        }

        private bool CheckIfCouponUsedPreviously(string patientId, int CouponId)
        {
            return _unitOfWork.Bookings.IsExist( b => b.PatientId ==  patientId &&
            b.DiscountCodeCouponId == CouponId );
        }

        private bool CheckMinimumBookings(string patientId, int? minimumRequiredRequests)
        {
            int NumberOfPatientBookings = _unitOfWork.Bookings.NumOfBookings(
                                                        b => b.PatientId == patientId);
            return NumberOfPatientBookings >= minimumRequiredRequests;
        }

        #region base methods
        public IActionResult GetAll(int Page, int PageSize, string search)
        {
            return _unitOfWork.ApplicationUser.GetAll(Page, PageSize, search);
        }
        #endregion

    }
}