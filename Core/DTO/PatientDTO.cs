using Core.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class PatientDTO
    {
        public string ImagePath;
        public string FullName;
        public string Email;
        public string Phone;
        public string Gender;
        public string DateOfBirth;
        public Image? Image;
        public List<BookingDTO>? Bookings;
    }
}
