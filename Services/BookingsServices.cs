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

        public BookingsServices(IUnitOfWork UnitOfWork) {
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

        public IActionResult AddBookingToPatient(int AppointmentTimeId, string DiscountCodeCouponName)
        {
            throw new NotImplementedException();
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