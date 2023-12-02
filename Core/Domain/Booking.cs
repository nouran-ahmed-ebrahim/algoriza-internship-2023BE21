using Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Booking state is required.")]
        [EnumDataType(typeof(BookingState))]
        public BookingState? BookingState { get; set; }
        public AppointmentTime? AppointmentTime { get; set; }
        public Doctor? Doctor { get; set; }
       
        public ApplicationUser? Patient { get; set; }

        public DiscountCodeCoupon? DiscountCodeCoupon { get; set; }
    }
}
