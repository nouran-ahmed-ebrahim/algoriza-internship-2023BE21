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
    public class DoctorServices(IUnitOfWork UnitOfWork, IMapper mapper) : 
        ApplicationUserService(UnitOfWork, mapper), IDoctorServices
    {
        [Authorize(Roles ="Doctor")]
        public async Task<IActionResult> AddAppointments(int DoctorId,int price,
            Dictionary<string, List<DateTime>> Appointments)
        {
            //  set doctor price
            var SettingPriceResult = await SetPrice(DoctorId, price); 
            if (SettingPriceResult is not OkResult)
            {
                return SettingPriceResult;
            }

            // set Days 
            var AddingDaysResult = new AddDays(DoctorId, Appointments);
            if (AddingDaysResult is not OkResult)
            {
                return AddingDaysResult;
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
                var IsExist = _unitOfWork.Doctors.IsExist(id);
                if (IsExist is not OkObjectResult okResult)
                {
                    return IsExist;
                }

                Doctor doctor = okResult.Value as Doctor;

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
