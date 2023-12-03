using Core.Domain;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AppointmentTimeServices : IAppointmentTimeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentTimeServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult AddDayTime(int dayId, TimeSpan timeSlot)
        {
            AppointmentTime appointmentTime;
            // if the day exist previously check timeSlot existence
            if (dayId > 0)
            {
                appointmentTime = _unitOfWork.AppointmentTimes.GetByDayIdAndSlot(dayId, timeSlot);

                if (appointmentTime != null)
                {
                    return new OkResult();
                }
            }

            appointmentTime = new AppointmentTime()
            {
                Time = timeSlot
            };
            return new OkObjectResult(appointmentTime);

        }

        public IActionResult AddDayTimes(int dayId, List<string> Times)
        {    
            if(Times.Count == 0)
            {
                return new BadRequestObjectResult("Times is required");
            }

            List<AppointmentTime> dayTimes = new List<AppointmentTime>();
            IActionResult result;
            TimeSpan timeSlot;
            foreach (var time in Times)
            {
                timeSlot = ConvertStringTotTimeSpan(time);
                result = AddDayTime(dayId, timeSlot);
                if (result is not OkObjectResult okObject)
                {
                    continue;
                }
                dayTimes.Add(okObject.Value as AppointmentTime);
            }

            return new OkObjectResult(dayTimes);
        }

        public TimeSpan ConvertStringTotTimeSpan(string strTime)
        { 
            return Convert.ToDateTime(strTime).TimeOfDay; ;
        }
    }
}
