using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplictationDbContext: DbContext
    {
        public ApplictationDbContext(DbContextOptions<ApplictationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>()
            .HasIndex(p => p.Name)
            .IsUnique();
        }
        public DbSet<Person> Persons { get; set; }
       public DbSet<AppointmentDay> AppointmentDays { get; set; }
        public DbSet<AppointmentTime> AppointmentTimes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DiscountCodeCoupon> DiscountCodeCoupons { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
    }
}
