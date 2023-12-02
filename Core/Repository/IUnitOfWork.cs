﻿using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public IDataOperationsRepository<Doctor> Doctors { get; }
        public IApplicationUserRepository ApplicationUser { get; }
        public IDiscountCodeCouponRepository DiscountCodeCoupons { get; }
        public IDataOperationsRepository<Appointment> Appointments { get; }
        public IBookingsRepository Bookings { get; }
        public ISpecializationRepository Specializations { get; }
        int Complete();
    }
}
