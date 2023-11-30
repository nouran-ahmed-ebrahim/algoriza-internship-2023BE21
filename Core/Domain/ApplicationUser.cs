using Core.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public int Id { get; set; }

        [RequiredAttributeForDoctor(ErrorMessage = "This property is required for users with the 'doctor' role.")]
        public byte[] Image { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public List<Booking>? Requests { get; set; }
    }
}

