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
        public IDoctorRepository Doctors { get; }
        public IApplicationUserRepository ApplicationUser { get; }
        public IDataOperationsRepository<DiscountCodeCoupon> DiscountCodeCoupons { get; }
        public IAppointmentRepository Appointments { get; }
        public IBookingsRepository Bookings { get; }
        public ISpecializationRepository Specializations { get; }
        int Complete();
    }
}
