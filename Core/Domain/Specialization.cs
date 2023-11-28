using Azure.Core;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Specialization
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int NumOfRequests
        {
            get => Doctors.Sum(x => x.NumOfRequests);
            private set { }
        }
        public List<Doctor> Doctors { get; set; }
        //seeding
    }
}
