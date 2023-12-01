using Core.Utilities;
using Microsoft.EntityFrameworkCore;
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
        public string Name { get; set; }

        [Required]
        public bool IsActivated { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than or equal to 0")]
        public int Value { get; set; }

        [Required]
        [EnumDataType(typeof(DiscountType))]
        public DiscountType DiscountType { get; set; }

        [Required]
        public int MinimumRequiredRequests {  get; set; }
        public List<Booking>? Requests { get; set; } 
    }
}
