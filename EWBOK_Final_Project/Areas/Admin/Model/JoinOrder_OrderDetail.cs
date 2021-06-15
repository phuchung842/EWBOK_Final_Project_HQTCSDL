using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWBOK_Final_Project.Areas.Admin.Model
{
    public class JoinOrder_OrderDetail
    {
        public Order Order { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}