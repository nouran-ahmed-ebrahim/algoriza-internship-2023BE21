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
        private IUnitOfWork _unitOfWork;

        public BookingsServices(IUnitOfWork UnitOfWork) {
            _unitOfWork = UnitOfWork;
        }
        public async Task<IActionResult> NumOfBookings()
        {
            Task<int> totalBookings = _unitOfWork.Bookings.NumOfBooKings();
            Task<int> pendingBookings= _unitOfWork.Bookings.NumOfBookings((Booking b) => b.BookingState == BookingState.Pending);
            Task<int> completedBookings = _unitOfWork.Bookings.NumOfBookings((Booking b) => b.BookingState == BookingState.Completed);
            Task<int> cancelledBookings = _unitOfWork.Bookings.NumOfBookings((Booking b) => b.BookingState == BookingState.Cancelled);

            var Result = new
            {
                TotalBookings = totalBookings,
                PendingBookings = pendingBookings,
                CompletedBookings = completedBookings,
                CancelledBookings= cancelledBookings
            };
            return new OkObjectResult(Result);
        }
    }
}