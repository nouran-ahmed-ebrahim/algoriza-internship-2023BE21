using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DoctorServices: ApplicationUserService, IDoctorServices
    {
        private readonly IAppointmentServices _appointmentServices;

        public DoctorServices(IUnitOfWork UnitOfWork, IMapper mapper, 
            IAppointmentServices appointmentServices) : base(UnitOfWork, mapper)
        {
            _appointmentServices = appointmentServices;
        }


        [Authorize(Roles ="Doctor")]
        public IActionResult AddAppointments(int DoctorId,int price,
            Dictionary<DayOfWeek, List<DateTime>> Appointments)
        {
            //  set doctor price
            var SettingPriceResult = SetPrice(DoctorId, price); 
            if (SettingPriceResult is not OkResult)
            {
                return SettingPriceResult;
            }

            // set Days 
            var AddingDaysResult = _appointmentServices.AddDays(DoctorId, Appointments);
            if (AddingDaysResult is not OkResult)
            {
                return AddingDaysResult;
            }

            _unitOfWork.Complete();
            return new OkResult();
        }

        public IActionResult SetPrice(int doctorId, int price)
        {
            Doctor doctor = _unitOfWork.Doctors.GetById(doctorId);
            if (doctor == null)
            {
                return new NotFoundObjectResult($"Doctor with id {doctorId} is not found");

            }

            doctor.Price = price;

            var updatingResult = _unitOfWork.Doctors.Update(doctor);
            if (updatingResult is not OkResult)
            {
                return updatingResult;
            }

            return new OkResult();
        }

        public async Task<IActionResult> AddDoctor(UserDTO userDTO, UserRole patient, string specialize)
        {
            // get specializeId by name
            Specialization specialization = _unitOfWork.Specializations.GetByName(specialize);
            if(specialization == null)
            {
                return new BadRequestObjectResult($"There is no Specialization called {specialize}");
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
            try
            {
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
                if (doctor != null)
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
    }
}
