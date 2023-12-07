using Core.Utilities;

namespace Core.DTO
{
    public class BookingDTO
    {
       public string ImagePath;
       public string DoctorName;
       public string Specialization;
       public string Day;
       public string Time;
       public decimal price;
       public string discoundCodeName;
       public int CouponValue;
       public DiscountType DiscountType;
       public string BookingStatus;
    }
}