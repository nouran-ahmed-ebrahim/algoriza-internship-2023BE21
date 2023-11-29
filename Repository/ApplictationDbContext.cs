using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    // To merge IdentityDB Wth myDB 
    public class ApplictationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplictationDbContext(DbContextOptions<ApplictationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>()
            .HasIndex(p => p.Name)
            .IsUnique();

            modelBuilder.Entity<Specialization>()
             .HasData( new List<Specialization>
                {
                    new Specialization
                    {
                        Id =  1,
                        Name="Psychiatry(Mental, Emotional or Behavioral Disorders)"
                    },
                    new Specialization
                    {
                        Id =  2,
                        Name="Dentistry(Teeth)"
                    },
                    new Specialization
                    {
                        Id =  3,
                        Name="Pediatrics and New Born(Child)"
                    },
                   new Specialization
                    {
                       Id =  4,
                       Name="Orthopedics(Bones)"
                    },
                    new Specialization
                    {
                        Id =  5,
                        Name="Genecology and Infertility"
                    },
                    new Specialization
                    {
                        Id =  6,
                        Name="Ear, Nose and Throat"
                    },
                   new Specialization
                    {
                        Id =  7,
                        Name="Andrology and Male Infertility"
                    },
                    new Specialization
                    {
                        Id =  8,
                        Name="Allergy and Immunology(Sensitivity and Immunity)"
                    },
                    new Specialization
                    {
                        Id =  9,
                        Name="Cardiology and Vascular Disease(Heart)"
                    },
                   new Specialization
                    {
                        Id = 10,
                        Name="Audiology"
                    },
                    new Specialization
                    {
                        Id =  11,
                        Name="Cardiology and Thoracic Surgery(Heart & Chest)"
                    },
                    new Specialization
                    {
                        Id = 12,
                        Name="Chest and Respiratory"
                    },
                    new Specialization
                    {
                        Id =  13,
                        Name="Dietitian and Nutrition"
                    },
                    new Specialization
                    {
                        Id =  14,
                        Name="Diagnostic Radiology(Scan Centers)"
                    },
                    new Specialization
                    {
                        Id =  15,
                        Name="Diabetes and Endocrinology"
                    }
                });

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
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
