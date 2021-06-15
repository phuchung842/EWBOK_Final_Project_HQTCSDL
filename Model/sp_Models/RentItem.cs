using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.sp_Models
{
    public class RentItem
    {
        public long ProductID { get; set; }
        public short DayRent { get; set; }
        public decimal RentMoney { get; set; }
        public decimal Deposit { get; set; }
    }
    public class RentItemWithoutRegis
    {
        public RentItem Item { get; set; }
        public int Quantity { get; set; }
    }
}
