using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class AppointmentDay
    {
        public int Id { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        public Appointment Appointment { get; set; }
        public List<AppointmentTime> Time { get; set; }
    }
}
