using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DoctorServices: ApplicationUserService, IDoctorServices
    {
        private readonly IAppointmentServices _appointmentServices;

        public DoctorServices(IUnitOfWork UnitOfWork, IMapper mapper,
            IAppointmentServices appointmentServices) : 
            base(UnitOfWork, mapper)
        {
            _appointmentServices = appointmentServices;
        }

        public IActionResult GetTop10()
        {
            return _unitOfWork.Doctors.GetTop10Doctors();
        }
        public IActionResult AddAppointments(int DoctorId, AppointmentsDTO appointments)
        {
            //  set doctor price
            var SettingPriceResult = SetPrice(DoctorId, appointments.Price); 
            if (SettingPriceResult is not OkObjectResult)
            {
                return SettingPriceResult;
            }

            // set DayOfWeek 
            var AddingDayOfWeekResult = _appointmentServices.AddDays(DoctorId, appointments.days);
            if (AddingDayOfWeekResult is not OkResult)
            {
                return AddingDayOfWeekResult;
            }

            _unitOfWork.Complete();
            return new OkObjectResult("Price & Appointments Added Successfully");
        }

        private IActionResult SetPrice(int doctorId, decimal price)
        {
            Doctor doctor = _unitOfWork.Doctors.GetById(doctorId);
            if (doctor == null)
            {
                return new NotFoundObjectResult($"Doctor with id {doctorId} is not found");

            }

            doctor.Price = price;

            var updatingResult = _unitOfWork.Doctors.Update(doctor);
            return updatingResult;
           
        }

        public async Task<IActionResult> AddDoctor(UserDTO userDTO, UserRole patient, string specialize)
        {
            try
            {
                // get specializeId by name
                Specialization specialization = _unitOfWork.Specializations.GetByName(specialize);
                if (specialization == null)
                {
                    return new NotFoundObjectResult($"There is no Specialization called {specialize}");
                }

                // create user
                var result = await AddUser(userDTO, UserRole.Doctor);

                //User Creation Failed
                if (result is not OkObjectResult okResult)
                {
                    return result;
                }


                ApplicationUser User = okResult.Value as ApplicationUser;
                Doctor doctor = new()
                {
                    DoctorUser = User,
                    Specialization = specialization,
                };

                await _unitOfWork.Doctors.Add(doctor);
                _unitOfWork.Complete();
                return new OkObjectResult(doctor);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while Adding Doctor \n: {ex.Message}" +
                    $"\n {ex.InnerException?.Message}")
                {
                    StatusCode = 500
                };
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            // delete doctor (it will delete user also because the delete action is on cascade
            try
            {
                Doctor doctor = _unitOfWork.Doctors.GetById(id);
                if (doctor == null)
                {
                    return new NotFoundObjectResult($"Id {id} is not found");
                }

                _unitOfWork.Doctors.Delete(doctor);
                 ApplicationUser User = await _unitOfWork.Doctors.GetDoctorUser(doctor.DoctorUserId);
                await _unitOfWork.ApplicationUser.DeleteUser(User);
                _unitOfWork.Complete();
                return new OkObjectResult("Deleted successfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public IActionResult ConfirmCheckUp(int BookingId)
        {
            return ChangeBookingState(BookingId, BookingState.Completed);
        }

        public async Task<IActionResult> UpdateDoctor(int id, UserDTO userDTO, string specialize)
        {
            try
            {
                // Get Old Data
                Doctor doctor = _unitOfWork.Doctors.GetById(id);
                if(doctor == null)
                {
                    return new NotFoundObjectResult($"There is no Doctor with id: {id}.");
                }

                // Get & Set new specializeId
                Specialization specialization = _unitOfWork.Specializations.GetByName(specialize);
                if (specialization == null)
                {
                    return new NotFoundObjectResult($"There is no Specialization called {specialize}");
                }
                doctor.SpecializationId = specialization.Id;

                // Update User
                ApplicationUser user = await _unitOfWork.ApplicationUser.GetUser(doctor.DoctorUserId);
                var result = await UpdateUser(user, userDTO);

                //User Creation Failed
                if (result is not OkResult)
                {
                    return result;
                }

                _unitOfWork.Doctors.Update(doctor);
                _unitOfWork.Complete();
                return new OkObjectResult(doctor);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while Adding Doctor \n: {ex.Message}" +
                    $"\n {ex.InnerException?.Message}")
                {
                    StatusCode = 500
                };
            }
        }

        public IActionResult GetById(int id)
        {
            // check Id Existence
            bool IfFound = _unitOfWork.Doctors.IsExist(doctor => doctor.Id == id);
            if (!IfFound)
            {
                return new NotFoundObjectResult($"No doctor found with id {id}");
            }

            var result = _unitOfWork.Doctors.GetDoctorInfo(id);
            if (result is not OkObjectResult okResult)
            {
                return result;
            }


            DoctorDTO doctorInfo = okResult.Value as DoctorDTO;

            var ImageConvertingResult = GetImage(doctorInfo.Image);
            if (ImageConvertingResult is not OkObjectResult ImageObject)
            {
                return ImageConvertingResult;
            }

            var CompleteDoctorInfo = new
            {
                Image = (ImageObject.Value as Image),
                FullName = doctorInfo.FullName,
                Email = doctorInfo.Email,
                Phone = doctorInfo.Phone,
                Gender = doctorInfo.Gender,
                BirthOfDate = doctorInfo.BirthOfDate,
                Specialization = doctorInfo.Specialization
            };

            return new OkObjectResult(CompleteDoctorInfo);
        }
    }
}
