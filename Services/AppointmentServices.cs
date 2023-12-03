﻿using Core.Domain;
using Core.Repository;
using Core.Services;
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

        public IActionResult AddDays(int doctorId, Dictionary<DayOfWeek, List<TimeSpan>> appointments)
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

        public IActionResult AddDay(int doctorId, KeyValuePair<DayOfWeek, List<TimeSpan>> day)
        {
            if (day.Value == null)
            {
                return new BadRequestObjectResult($"Inter Time Slots for day {day}");
            }

            Appointment appointment = _unitOfWork.Appointments.GetByDoctorIdAndDay(doctorId, day.Key);
            int DayId;

            if (appointment == null)
            {
                appointment = new Appointment()
                {
                    DoctorId = doctorId,
                    DayOfWeek = day.Key,
                };
                DayId = _unitOfWork.Appointments.GetNextAppointmentId();
                try
                {
                    _unitOfWork.Appointments.Add(appointment);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult($"There is a problem while Adding {day} \n {ex.Message}" +
                         $"\n {ex.InnerException?.Message}");
                }
            }
            else
            {
                DayId = appointment.Id;
            }

            IActionResult addingDayTimesResult = _timeServices.AddDayTimes(DayId, day.Value);
            if (addingDayTimesResult is not OkResult)
            {
                return new OkResult();
            }

            return new OkResult();
        }

    }
}