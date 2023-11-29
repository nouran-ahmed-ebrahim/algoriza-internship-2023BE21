using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<Doctor> Doctors { get; }
        public IPatientsRepository Patients { get; }
        public IBaseRepository<DiscountCodeCoupon> DiscountCodeCoupons { get; }
        public IBaseRepository<Appointment> Appointments { get; }
        public IBookingsRepository Bookings { get; }
        public ISpecializationRepository Specializations { get; }
        int Complete();
    }
}
