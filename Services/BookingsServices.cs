using Core.Domain;
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
        private readonly IDiscountCodeCouponServices _couponServices;
        private readonly IAppointmentServices _appointmentServices;

        public BookingsServices(IUnitOfWork UnitOfWork, IDiscountCodeCouponServices CouponServices,
            IAppointmentServices AppointmentServices) {
            _unitOfWork = UnitOfWork;
            _couponServices = CouponServices;
            _appointmentServices = AppointmentServices;
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
            bool IsAvailable =  _appointmentServices.CheckAppointmentAvailablity(AppointmentTimeId);
            if(!IsAvailable)
            {
                return new BadRequestObjectResult("AppointmentTime is held by another patient");
            }

            // Get DiscountCoupon
            DiscountCodeCoupon discountCodeCoupon = _unitOfWork.DiscountCodeCoupons.GetByName(DiscountCodeCouponName);

            // Check if it applicable
            var ValiditionReult = _couponServices.CheckCouponApplicablty(discountCodeCoupon, PatientId);
            if(ValiditionReult is not OkResult)
            {
                return ValiditionReult;
            }

            // Add Booking
            Booking NewBooking = new()
            {
                PatientId = PatientId,
                AppointmentTimeId = AppointmentTimeId,
                DiscountCodeCouponId = discountCodeCoupon.Id,
            };

            _unitOfWork.Bookings.Add(NewBooking);
            _unitOfWork.Complete();

            return new OkObjectResult(NewBooking);
        }

        public IActionResult ChangeBookingState(int BookingId, BookingState bookingState)
        {
            Booking booking = _unitOfWork.Bookings.GetById(BookingId);
            if (booking == null)
            {
                return new NotFoundObjectResult("Booking Id {BookingId} is not exist");
            }

            booking.BookingState = bookingState;
            try
            {
                _unitOfWork.Bookings.Update(booking);
                _unitOfWork.Complete();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult($"There is problem during booking confirmation")
                {
                    StatusCode = 500
                };
            }
        }

        #region base methods
        public IActionResult GetAll(int Page, int PageSize, string search)
        {
            return _unitOfWork.ApplicationUser.GetAll(Page, PageSize, search);
        }
        #endregion

    }
}