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
       public string price;
       public string discoundCodeName;
       public int CouponValue;
       public DiscountType DiscountType;
       public string BookingStatus;
    }
}