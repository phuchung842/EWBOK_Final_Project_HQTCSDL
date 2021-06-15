using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWBOK_Final_Project.Areas.Admin.Model
{
    public class JoinOrder_User
    {
        public Order Order { get; set; }
        public User User { get; set; }
    }
}