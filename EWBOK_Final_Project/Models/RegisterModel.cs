using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EWBOK_Final_Project.Models
{
    public class RegisterModel
    {
        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Cần có tên đăng nhập")]
        public string Username { get; set; }

        [DisplayName("Mật khẩu")]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "Mật khẩu có ít nhất là 5 ký tự")]
        [Required(ErrorMessage = "Cần có mật khẩu")]
        public string Password { get; set; }

        [DisplayName("Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận khẩu chưa chính xác")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Cần có họ tên")]
        public string Name { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Cần có Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [DisplayName("Điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại chưa hợp lệ")]
        public string Phone { get; set; }
    }
}