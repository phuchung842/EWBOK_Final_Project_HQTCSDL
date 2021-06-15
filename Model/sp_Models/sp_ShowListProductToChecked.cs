using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.sp_Models
{
    public class sp_ShowListProductToChecked
    {
        public long ProductID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int QuantityChoose { get; set; }

        [Range(1,100,ErrorMessage ="Số ngày thuê không hợp lệ")]
        public short DayRent { get; set; }
        public bool IsChecked { get; set; }
    }
    public class ProductToCheckedModel
    {
        public List<sp_ShowListProductToChecked> ListProductToCheckedModel { get; set; }
    }
}
