using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PatientServices(IUnitOfWork UnitOfWork, IMapper mapper) :
        ApplicationUserService(UnitOfWork, mapper), IPatientServices
    {
        
        public IActionResult CancelBooking(int BookingId)
        {
            return ChangeBookingState(BookingId, BookingState.Cancelled);
        }

        public async Task<IActionResult> GetAllPatients(int Page, int PageSize, string search)
        {
            try
            {
                Func<PatientDTO, bool> criteria = null;

                if (!string.IsNullOrEmpty(search))
                    criteria = (d => d.Email.Contains(search) || d.Phone.Contains(search) ||
                                d.FullName.Contains(search) || d.Gender.Contains(search) ||
                                d.DateOfBirth.Contains(search));

                // get patients
                var gettingPatientsResult = await  _unitOfWork.Patients.GetAllPatients(Page, PageSize, criteria);
                if (gettingPatientsResult is not OkObjectResult patientsResult)
                {
                    return gettingPatientsResult;
                }

                List<PatientDTO> doctorsInfoList = patientsResult.Value as List<PatientDTO>;

                if (doctorsInfoList == null || doctorsInfoList.Count() == 0)
                {
                    return new NotFoundObjectResult("There is no patients with this conditions");
                }

                // Load doctor images
                var doctorsInfo = doctorsInfoList.Select(d => new 
                {
                    Image = GetImage(d.ImagePath),
                    d.FullName,
                    d.Phone,
                    d.Email,
                    d.Gender,
                    d.DateOfBirth
                }).ToList();

                return new OkObjectResult(doctorsInfo);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while Getting Patients info \n: {ex.Message}" +
                    $"\n {ex.InnerException?.Message}")
                {
                    StatusCode = 500
                };
            }
        }
    
        public async Task<IActionResult> GetById(string Id)
        {
            try
            {
                // check if the id exist
                bool IsExist = _unitOfWork.Patients.IsExist(Id);
                if (!IsExist)
                {
                    return new NotFoundObjectResult($"There is no patient with Id {Id}");
                }

                // Get patient info
                ApplicationUser patientUser =  await _unitOfWork.Patients.GetUser(Id);

                // Get Patient bookings
                IActionResult GettingPatientBookings = GetPatientBookings(Id);
                if (GettingPatientBookings is not OkObjectResult BookingsObject)
                {
                    return GettingPatientBookings;
                }

                var PatientBookings = BookingsObject.Value ;

                // Load Booking'S Doctors image & Calculate  final price
                var patient = new
                {
                    Image = GetImage(patientUser.Image),
                    patientUser.FullName,
                    patientUser.PhoneNumber,
                    patientUser.Email,
                    patientUser.DateOfBirth,
                    patientUser.Gender,
                    Bookings = PatientBookings
                };

                return new OkObjectResult(patient);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while Getting Patient info \n: {ex.Message}" +
                   $"\n {ex.InnerException?.Message}")
                {
                    StatusCode = 500
                };
            }
        }

        public IActionResult GetPatientBookings(string Id)
        {
            try
            {
                // check if the id exist
                bool IsExist = _unitOfWork.Patients.IsExist(Id);
                if (!IsExist)
                {
                    return new NotFoundObjectResult($"There is no patient with Id {Id}");
                }

                //get bookings
                var gettingBookingsResult =  _unitOfWork.Bookings.GetPatientBookings(Id);
                if (gettingBookingsResult is not OkObjectResult BookingsObject)
                {
                    return gettingBookingsResult;
                }

                List<BookingDTO> bookings = BookingsObject.Value as List<BookingDTO>;
                
                if(bookings == null || bookings.Count() == 0)
                {
                    return new NotFoundObjectResult("There is no bookings for the patient");
                }

                // load Doctors' Image & calculate final price
                var FullBookingsInfo = bookings.Select(b => new
                {
                    Image = GetImage(b.ImagePath),
                    b.DoctorName,
                    b.Specialization,
                    b.BookingStatus,
                    b.Day,
                    b.Time,
                    b.discoundCodeName,
                    FinalPrice = CalculateFinalPrice(b.price, b.CouponValue, b.DiscountType)
                });

                return new OkObjectResult(FullBookingsInfo);        
            }
            catch(Exception ex)
            {
                return new ObjectResult($"An error occurred while Getting Patient's Bookings \n: {ex.Message}" +
                   $"\n {ex.InnerException?.Message}")
                {
                    StatusCode = 500
                };
            }
        }

        private decimal CalculateFinalPrice(decimal price, int couponValue, DiscountType discountType)
        {
            decimal DiscountValue = 0;
            
            if(discountType == DiscountType.Value)
                DiscountValue =couponValue;
            else
            {
                DiscountValue = (price * couponValue) / 100;
            }

            return price - DiscountValue;
        }
    }
}
