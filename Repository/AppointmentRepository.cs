using Core.Domain;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppointmentRepository(ApplicationDbContext context) :
            BaseRepository<Appointment>(context), IAppointmentRepository
    {
        public int GetNextDoctorId()
        {
            var maxId = _context.Appointments.Select(d => d.Id).DefaultIfEmpty(0).Max();
            return (maxId + 1);
        }
    }
}
