using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class AppointmentTime
    {
        public int Id { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [ForeignKey("FK_AppointmentDayId")]
        public AppointmentDay AppointmentDay { get; set; }
    }
}
