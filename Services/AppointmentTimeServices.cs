﻿using Core.Domain;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AppointmentTimeServices : IAppointmentTimeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentTimeServices(IUnitOfWork _unitOfWork) { 
        }
        public IActionResult AddingDayTime(int dayId, DateTime timeSlot)
        {
            try
            {
                AppointmentTime appointmentTime = new AppointmentTime()
                {
                    AppointmentId = dayId,
                    Time = timeSlot
                };
                _unitOfWork.AppointmentTimes.Add();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"There is a problem while adding time slot " +
                    $"{timeSlot} \n {ex.Message} : \n {ex.InnerException?.Message}");
            }

            
        }
    }
}
