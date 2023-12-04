using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    // To merge IdentityDB Wth myDB 
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Index
            modelBuilder.Entity<Specialization>()
            .HasIndex(p => p.Name)
            .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<DiscountCodeCoupon>()
            .HasIndex(c => c.Name)
            .IsUnique();
            #endregion

            modelBuilder.Entity<Doctor>()
            .Property(d => d.Price)
            .HasColumnType("decimal(18, 2)");

            #region relations

            modelBuilder.Entity<Booking>()
            .HasOne<AppointmentTime>()
            .WithMany()
            .HasForeignKey(p => p.AppointmentTimeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Bookings_AppointmentTimes_AppointmentTimeId");

            modelBuilder.Entity<Booking>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Bookings_AspNetUsers_PatientId");


            modelBuilder.Entity<Booking>()
            .HasOne<DiscountCodeCoupon>()
            .WithMany()
            .HasForeignKey(b => b.DiscountCodeCouponId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Bookings_DiscountCodeCoupons_DiscountCodeCouponId");

         
             modelBuilder.Entity<Booking>()
            .HasOne<Doctor>()
            .WithMany()
            .HasForeignKey(b => b.DoctorId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Bookings_Doctors_DoctorId");


            #endregion

            #region dataSeeding

            modelBuilder.Entity<Specialization>()
             .HasData(new List<Specialization>
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
            
            // to prevent reeeding data
            var appliedMigrations = Database.GetAppliedMigrations();
            var isInitialMigration = appliedMigrations.Count() == 0;

            if (isInitialMigration)
            {
                modelBuilder.Entity<IdentityRole>()
                .HasData(new List<IdentityRole>
                {
                    new IdentityRole{Name = "Admin" },
                    new IdentityRole{Name = "Doctor"},
                    new IdentityRole{Name = "Patient"},
                });
                //await _roleManager.CreateAsync(new IdentityRole() { Name = "Patient" });
                //await _roleManager.CreateAsync(new IdentityRole() { Name = "Doctor" });
                //await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });

                modelBuilder.Entity<ApplicationUser>()
                .HasData(new ApplicationUser
                {
                    UserName = "Admin Admin",
                    DateOfBirth = new DateTime(2001, 5, 8),
                    Email = "admin@gmail.com",
                    PhoneNumber = "1234567890",
                    Gender = Core.Utilities.Gender.Female,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "123456")
                });
            }
            #endregion
        }
        public DbSet<AppointmentTime> AppointmentTimes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DiscountCodeCoupon> DiscountCodeCoupons { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
    }
}
