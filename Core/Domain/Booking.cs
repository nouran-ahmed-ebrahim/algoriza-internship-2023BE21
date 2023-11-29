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

        [Required]
        public RequestState RequestState { get; set; }
        public AppointmentTime AppointmentTime { get; set; }
        public Doctor Doctor { get; set; }
       
        [ForeignKey("FK_Requests_Patient_PatientId")]
        public int PatientId { get; set; }
        public ApplicationUser Patient { get; set; }

        [ForeignKey("FK_Requests_DiscountCodeCoupons_DiscountCodeCouponId")]
        public int? DiscountCodeCouponId { get; set; }
        public DiscountCodeCoupon? DiscountCodeCoupon { get; set; }
    }
}
