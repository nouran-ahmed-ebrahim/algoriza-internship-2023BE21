using Core.Utilities;
using System.Drawing;

namespace Core.DTO
{
    public class BookingDTO
    {
       public Image? Image;
       public string ImagePath;
       public string doctorName;
       public string specialization;
       public string day;
       public string time;
       public string price;
       public string discoundCodeName;
       public int CouponValue;
       public DiscountType DiscountType;
       public int finalPrice;
       public string BookingStatus;
    }
}