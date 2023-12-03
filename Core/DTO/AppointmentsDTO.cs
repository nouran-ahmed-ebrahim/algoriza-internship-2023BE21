using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class AppointmentsDTO
    {
        public decimal Price { get; set; }
        public List<DaySchedule>? Days { get; set; }
    }

    public class DaySchedule
    {
        public string? Day { get; set; }
        public List<string>? Times { get; set; }
    }

}
