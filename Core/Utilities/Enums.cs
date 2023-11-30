﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public enum Days
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }

    public enum BookingState
    {
        Pending,
        Completed,
        Cancelled
    }

    public enum Gender
    {
        Male,
        Female
    }
    
    public enum DiscountType
    {
        Percentage,
        Value
    }

    public enum UserState
    {
        Patient,
        Admin,
        Doctor
    }
}
