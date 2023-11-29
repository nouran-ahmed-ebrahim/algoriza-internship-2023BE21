using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IBookingRepository: IBaseRepository<ApplicationUser>
    {
        public Task<int> NumOfBooKings();
        public  Task<int> NumOfBookings(Expression<Func<Booking, bool>> criteria);

    }
}
