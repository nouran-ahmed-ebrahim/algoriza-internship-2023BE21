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
        public IBaseRepository<Doctor> Doctors { get; set; }
        public IBaseRepository<Patient> Patients { get; set; }
        public IBaseRepository<DiscountCodeCoupon> DiscountCodeCoupons { get; set; }
        public IBaseRepository<Appointment> Appointments { get; set; }
        public IBaseRepository<Request> Requests { get; set; }
        public ISpecializationRepository Specializations { get; set; }
        int Complete();
    }
}
