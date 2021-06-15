using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EWBOK_Final_Project.Models
{
    public class ChangePasswordModel
    {
        [DisplayName("Mật khẩu hiện tại")]
        [Required(ErrorMessage ="Cần nhập mật khẩu hiện tại")]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "Mật khẩu có ít nhất là 5 ký tự")]
        public string Password { get; set; }

        [DisplayName("Mật khẩu mới")]
        [Required(ErrorMessage ="Cần có mật khẩu mới")]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "Mật khẩu có ít nhất là 5 ký tự")]
        public string NewPassword { get; set; }

        [DisplayName("Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận khẩu mới chưa chính xác")]
        public string ConfirmNewPassword { get; set; }

    }
}