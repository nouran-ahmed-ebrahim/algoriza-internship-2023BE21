using Core.Domain;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingsRepository : BaseRepository<Booking>, IBookingsRepository
    {
        public BookingsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public Task<ApplicationUser> AddAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> NumOfCancelledRequests()
        {
            throw new NotImplementedException();
        }

        public Task<int> NumOfCompletedRequests()
        {
            throw new NotImplementedException();
        }

        public async Task<int> NumOfBookings(Expression<Func<Booking, bool>> criteria)
        {
            return await _context.Bookings.CountAsync(criteria);
        }

        public async Task<int> NumOfBooKings()
        {
            return await _context.Bookings.CountAsync();
        }

        public Task<ApplicationUser> UpdateAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ApplicationUser>> IBaseRepository<ApplicationUser>.GetAllAsync(int Page, int PageSize)
        {
            throw new NotImplementedException();
        }
    }

    
}
