using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWBOK_Final_Project.Areas.Admin.Model
{
    public class JoinProduct_OrderDetail
    {
        public Product Product { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}