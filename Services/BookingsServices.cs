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

        public void DeleteAsync(int id)
        {
            _unitOfWork.Bookings.Delete(id);
        }

        public IEnumerable<Booking> GetAllAsync(int Page, int PageSize)
        {
            return _unitOfWork.Bookings.GetAllAsync(Page, PageSize);
        }

        public Booking UpdateAsync(Booking entity)
        {
            return _unitOfWork.Bookings.Update(entity);
        }
    }
}