using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.sp_Models
{
    public class sp_ShowListProductRenting
    {
        public long UserID { get; set; }
        public long RentID { get; set; }
        public long RentProductID { get; set; }
        public long ProductID { get; set; }
        public DateTime DateExpire { get; set; }
        public string ProductName { get; set; }
        public string IDNumber { get; set; }
        public decimal RentMoney { get; set; }
        public decimal Deposit { get; set; }
        public decimal Penalty { get; set; }
        public bool IsChecked { get; set; }
    }
    public class RentingModel
    {
        public List<sp_ShowListProductRenting> ListReting { get; set; }
    }
}
