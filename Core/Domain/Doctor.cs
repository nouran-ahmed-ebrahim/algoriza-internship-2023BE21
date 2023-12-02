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

        #region ForeignKey
        [ForeignKey("FK_Doctors_AspNetUsers_DoctorUserId")]
        public string DoctorUserId { get; set; }

        [ForeignKey("FK_Doctors_Specializations_SpecializationId")]
        public int SpecializationId { get; set; }
        #endregion

        #region nav prop
        public ApplicationUser? DoctorUser { get; set; }
        public Specialization? Specialization { get; set; }
        #endregion
    }
}
