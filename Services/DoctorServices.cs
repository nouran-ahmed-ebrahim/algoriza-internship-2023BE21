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
    public class DoctorServices(IUnitOfWork UnitOfWork, IMapper mapper) : 
        ApplicationUserService(UnitOfWork, mapper), IDoctorServices
    {
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
                _unitOfWork.Doctors.Add(doctor);
                _unitOfWork.Complete();
                return new OkObjectResult(doctor);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while Adding Doctor \n: {ex.Message}")
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
                _unitOfWork.Complete();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
