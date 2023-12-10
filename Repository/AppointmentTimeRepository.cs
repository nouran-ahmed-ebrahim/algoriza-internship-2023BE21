﻿using Core.Domain;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppointmentTimeRepository(ApplicationDbContext context) :
                            DataOperationsRepository<AppointmentTime>(context), IAppointmentTimeRepository
    {
        public bool GetByDayIdAndSlot(int dayId, TimeSpan timeSlot)
        {
            return _context.AppointmentTimes.Any(t => t.AppointmentId == dayId &&
                                                              t.Time == timeSlot);
        }
    }
}
