using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Core.Repository
{
    public interface IBookingsRepository: ICommonRepository<Booking>
    {
        int NumOfBooKings();
        int NumOfBookings(Expression<Func<Booking, bool>> criteria);
        IActionResult GetPatientBookings(string PatientId);
    }
}
