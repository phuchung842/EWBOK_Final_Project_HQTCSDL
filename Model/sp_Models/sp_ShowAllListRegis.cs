using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.sp_Models
{
    public class sp_ShowAllListRegis
    {
        public string Name { get; set; }
        public long ProductID { get; set; }
        public string IDNumber { get; set; }
        public long UserID { get; set; }
        public int Quantity { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Need day rent")]
        public short DayRent { get; set; }
        public bool IsChecked { get; set; }
    }
    public class RegisModel
    {
        public List<sp_ShowAllListRegis> ListRegis { get; set; }
    }
}
