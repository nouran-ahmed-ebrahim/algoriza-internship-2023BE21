using Core.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than or equal to 0")]
        public int? Price { get; set; }

        [NotMapped]
        public int NumOfRequests => Requests.Count();
        public ApplicationUser? DoctorUser { get; set; }
        public Specialization? Specialization { get; set; }
        public List<Booking>? Requests { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
