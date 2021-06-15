using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.sp_Models
{
    public class sp_ShowListProductRent
    {
        public long RentID { get; set; }
        public long ProductID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string DateExpire { get; set; }
        public decimal RentMoney { get; set; }
        public decimal Deposit { get; set; }
        public decimal TotalRentMoney { get; set; }
        public decimal TotalDeposit { get; set; }
    }
}
