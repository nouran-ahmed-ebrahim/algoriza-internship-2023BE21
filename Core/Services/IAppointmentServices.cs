using Core.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAppointmentServices
    {
        IActionResult ConvertStringToDayOfWeek(string day);
        IActionResult AddDays(int doctorId, List<DaySchedule> appointments);
        IActionResult AddDay(int doctorId, DaySchedule day);
        bool CheckAppointmentAvailability(int appointmentTimeId);
    }
}
