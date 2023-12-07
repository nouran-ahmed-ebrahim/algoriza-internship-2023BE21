using Core.Utilities;

namespace Core.DTO
{
    public class BookingDTO
    {
       public string ImagePath;
       public string doctorName;
       public string specialization;
       public string day;
       public string time;
       public decimal price;
       public string discoundCodeName;
       public int CouponValue;
       public DiscountType DiscountType;
       public string BookingStatus;
    }
}