using Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than or equal to 0")]
        public int? Price { get; set; }

        [Required(ErrorMessage = "Day is required.")]
        [EnumDataType(typeof(DayOfWeek))]
        public DayOfWeek? DayOfWeek { get; set; }
        public Doctor? Doctor { get; set; }

    }
}
