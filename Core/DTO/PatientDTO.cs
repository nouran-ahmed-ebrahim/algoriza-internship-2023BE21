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
        public List<BookingDTO>? Bookings;
    }
}
