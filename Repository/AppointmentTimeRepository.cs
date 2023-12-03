using Core.Domain;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppointmentTimeRepository(ApplicationDbContext context) : 
                            BaseRepository<AppointmentTime>(context), IAppointmentTimeRepository
    {

    }
}
