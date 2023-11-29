﻿using Core.Domain;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public IBaseRepository<Doctor> Doctors { get; private set; }
        public IBaseRepository<Patient> Patients { get; private set; }
        public IBaseRepository<DiscountCodeCoupon> DiscountCodeCoupons { get; private set; }
        public IBaseRepository<Appointment> Appointments { get; private set; }
        public IBaseRepository<Request> Requests { get; private set; }
        public ISpecializationRepository Specializations { get; private set; }
        public UnitOfWork(ApplicationDbContext context) {
            _context = context;

            Doctors = new BaseRepository<Doctor>(_context);
            Patients = new BaseRepository<Patient>(_context);
            DiscountCodeCoupons = new BaseRepository<DiscountCodeCoupon>(_context);
            Appointments = new BaseRepository<Appointment>(_context);
            Requests = new BaseRepository<Request>(_context);
            Specializations = new SpecializationRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
