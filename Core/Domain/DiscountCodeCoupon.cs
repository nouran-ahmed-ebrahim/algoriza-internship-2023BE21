using Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class DiscountCodeCoupon
    {
        public int Id { get; set; }

        [Required]
        public bool IsActivated { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        [EnumDataType(typeof(DiscountType))]
        public DiscountType DiscountType { get; set; }

        public List<Booking> Requests { get; set; } 
    }
}
