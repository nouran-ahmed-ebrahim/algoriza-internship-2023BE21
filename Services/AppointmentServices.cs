using Core.Domain;
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
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IAppointmentTimeServices _timeServices;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentServices(IAppointmentTimeServices timeServices, IUnitOfWork unitOfWork)
        {
            _timeServices = timeServices;
            _unitOfWork = unitOfWork;
        }

        public IActionResult AddDays(int doctorId, Dictionary<DayOfWeek, List<DateTime>> appointments)
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

        public IActionResult AddDay(int doctorId, KeyValuePair<DayOfWeek, List<DateTime>> day)
        {
            Appointment appointment = new Appointment()
            {
                DoctorId = doctorId,
                DayOfWeek = day.Key,
            };

            try
            {
                _unitOfWork.Appointments.Add(appointment);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"There is a problem while Adding {day} \n {ex.Message}" +
                     $"\n {ex.InnerException?.Message}");
            }

            int DayId = _unitOfWork.Appointments.GetNextAppointmentId();
            IActionResult addingDayTimesResult = _timeServices.AddingDayTime(DayId, day.Value);
            if (addingDayTimesResult is not OkResult)
            {
                return new OkResult();
            }

            return new OkResult();
        }

    }
}
