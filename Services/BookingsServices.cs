using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Services
{
    public class BookingsServices : IBookingsServices
    {
        private IBookingRepository _bookingsRepository;

        public BookingsServices(IBookingRepository BookingsRepository) {
            _bookingsRepository = BookingsRepository;
        }
        public Task<IActionResult> NumOfBookings()
        {
            Task<int> totalBookings = _bookingsRepository.NumOfBooKings();
            Task<int> pendingBookings= _bookingsRepository.NumOfBookings();
            Task<int> completedBookings = _bookingsRepository.NumOfBookings();
            Task<int> CancelledBookings = _bookingsRepository.NumOfBookings();

            var result = new
            {
                TotalRequests = totalRequests,
                PendingRequests = pendingRequests,
                CompletedRequests = completedRequests
            };
            return
        }
    }
}