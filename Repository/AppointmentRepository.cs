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
        public Appointment GetByDoctorIdAndDay(int doctorId, DayOfWeek day)
        {
            return _context.Appointments.FirstOrDefault(a => a.DoctorId == doctorId && a.DayOfWeek == day);
        }

        public int GetNextAppointmentId()
        {
            var maxId = _context.Appointments.Select(d => d.Id).DefaultIfEmpty(0).Max();
            return (maxId + 1);
        }
    }
}
