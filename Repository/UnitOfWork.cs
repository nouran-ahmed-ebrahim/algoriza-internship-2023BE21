using Core.Domain;
using Core.Repository;
using Microsoft.AspNetCore.Identity;
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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IBaseRepository<Doctor> Doctors { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IBaseRepository<DiscountCodeCoupon> DiscountCodeCoupons { get; private set; }
        public IBaseRepository<Appointment> Appointments { get; private set; }
        public IBookingsRepository Bookings { get; private set; }
        public ISpecializationRepository Specializations { get; private set; }
        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

            Doctors = new BaseRepository<Doctor>(_context);
            ApplicationUser = new ApplicationUserRepository(_context, _userManager,roleManager);
            DiscountCodeCoupons = new BaseRepository<DiscountCodeCoupon>(_context);
            Appointments = new BaseRepository<Appointment>(_context);
            Bookings = new BookingsRepository(_context);
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
