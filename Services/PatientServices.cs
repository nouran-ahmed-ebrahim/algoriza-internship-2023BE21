using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PatientServices(IUnitOfWork UnitOfWork, IMapper mapper) : ApplicationUserService(UnitOfWork, mapper), IPatientServices
    {
        
        public IActionResult CancelBooking(int BookingId)
        {
            return ChangeBookingState(BookingId, BookingState.Cancelled);
        }

        public async Task<IActionResult> GetAllPatients(int Page, int PageSize, string search)
        {
            try
            {
                Func<ApplicationUser, bool> criteria = null;

                if (!string.IsNullOrEmpty(search))
                    criteria = (d => d.Email.Contains(search) || d.PhoneNumber.Contains(search) ||
                                d.FullName.Contains(search) || d.Gender.ToString().Contains(search) ||
                                d.DateOfBirth.ToString().Contains(search));

                // get patients
                var gettingPatientsResult = await  _unitOfWork.Patients.GetAllPatients(Page, PageSize, criteria);
                if (gettingPatientsResult is not OkObjectResult patientsResult)
                {
                    return gettingPatientsResult;
                }

                dynamic doctorsInfoList = patientsResult.Value  ;

                //// Load doctor images
                //var doctorsInfo = doctorsInfoList.Select(d => new UserDTO
                //{
                //    Image = GetImage(d.ImagePath),
                //    FullName = d.FullName,
                //    Phone = d.Phone,
                //    Email = d.Email,
                //    Gender = d.Gender,
                //}).ToList();

                return new OkObjectResult(doctorsInfoList);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while Getting Doctors info \n: {ex.Message}" +
                    $"\n {ex.InnerException?.Message}")
                {
                    StatusCode = 500
                };
            }
        }
    }
}
