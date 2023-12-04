using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IAppointmentTimeServices _timeServices;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentServices(IAppointmentTimeServices timeServices, IUnitOfWork unitOfWork)
        {
            _timeServices = timeServices;
            _unitOfWork = unitOfWork;
        }

        public IActionResult AddDays(int doctorId, List<DaySchedule> appointments)
        {
            IActionResult result;
            foreach (var day in appointments)
            {
                result = AddDay(doctorId, day);
                if (result is not OkResult)
                {
                    return result;
                }
            }
            return new OkResult();
        }

        public IActionResult ConvertStringToDayOfWeek(string day)
        {
            DayOfWeek dayOfWeek;
            if (Enum.TryParse(day, true, out dayOfWeek))
            {
                return new OkObjectResult(dayOfWeek);
            }
            else
            {
                return new BadRequestObjectResult("Day is invalid");
            }
        }
        public IActionResult AddDay(int doctorId, DaySchedule DaySchedule)
        {
            if (DaySchedule.Day == null)
            {
                return new BadRequestObjectResult($"Inter Time Slots for day {DaySchedule}");
            }

            var result = ConvertStringToDayOfWeek(DaySchedule.Day);

            if (result is not OkObjectResult okResult)
            {
                return result;
            }
            DayOfWeek dayOfWeek = (DayOfWeek)okResult.Value;

            Appointment appointment = _unitOfWork.Appointments.GetByDoctorIdAndDay(doctorId, dayOfWeek);
            int DayId = 0;
            if (appointment == null)
            {
                appointment = new Appointment()
                {
                    DoctorId = doctorId,
                    DayOfWeek = dayOfWeek,
                };
            }
            else
            {
                DayId = appointment.Id;
            }

         //   List<AppointmentTime> AppointmentTimes = appointment.AppointmentTimes;
            
            IActionResult addingDayTimesResult = _timeServices.AddDayTimes(DayId, DaySchedule.Times);
            if (addingDayTimesResult is not OkObjectResult addingTimesResult)
            {
                return addingDayTimesResult;
            }

            var addingTimesList = (List<AppointmentTime>)addingTimesResult.Value;
            if(addingTimesList.Count == 0)
            {
                return new OkResult();
            }
            appointment.AppointmentTimes = addingTimesList; 

            try
            {
                if (DayId == 0)
                {
                    _unitOfWork.Appointments.Add(appointment);
                }
                else
                {
                    _unitOfWork.Appointments.Update(appointment);
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"There is a problem while Adding {DaySchedule} \n {ex.Message}" +
                     $"\n {ex.InnerException?.Message}");
            }
        }

        public bool CheckAppointmentAvailability(int appointmentTimeId)
        {
            bool IsHeld= _unitOfWork.Bookings.IsExist(a => a.AppointmentTimeId == appointmentTimeId &&
             a.BookingState == BookingState.Pending);

            return !IsHeld;
        }
    }
}
