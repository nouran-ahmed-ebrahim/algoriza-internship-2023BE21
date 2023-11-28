using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Appointment
    {
        public int Id { get; set; }

        public Doctor Doctor { get; set; }
        public List<Appointment> Appointments { get; set; }

    }
}
