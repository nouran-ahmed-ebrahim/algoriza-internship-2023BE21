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
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        public int Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int NumOfRequests
        {
            get => Requests.Select(r => r.RequestState == RequestState.Completed).Count();
            private set { }
        }
        public Person Person { get; set; }
        public Specialization Specialization { get; set; }
        public List<Request> Requests { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
