using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EWBOK_Final_Project.Areas.Admin.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập Username")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mời nhập Password")]
        public string PassWord { set; get; }

        public bool RememberMe { set; get; }
    }
}