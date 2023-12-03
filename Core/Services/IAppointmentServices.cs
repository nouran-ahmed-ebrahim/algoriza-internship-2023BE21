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
        IActionResult AddDays(int doctorId, Dictionary<DayOfWeek, List<DateTime>> appointments);
        IActionResult AddDay(int doctorId, KeyValuePair<DayOfWeek, List<DateTime>> day);
    }
}
