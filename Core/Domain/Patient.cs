using Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Patient
    {
        public int Id { get; set; }

        [NotMapped]
        public int CompletedRequests => Requests.Select(
            r => r.RequestState == RequestState.Completed).Count();
        public Person Person { get; set; }
        public List<Request> Requests { get; set; }
    }
}
