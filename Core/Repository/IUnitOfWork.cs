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
        public IBaseRepository<Patient> Patients { get; }
        public IBaseRepository<DiscountCodeCoupon> DiscountCodeCoupons { get; }
        public IBaseRepository<Appointment> Appointments { get; }
        public IBaseRepository<Request> Requests { get; }
        public ISpecializationRepository Specializations { get; }
        int Complete();
    }
}
