using EWBOK_Final_Project.Common;
using EWBOK_Final_Project.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            new LogDao().SetLog("Logout", null, ((User)Session[Constants.USER_INFO]).ID);
            Session[Constants.USER_INFO] = null;
            Session[Constants.USER_SESSION] = null;
            Session[Constants.WEBSITE_COLOR] = null;
            return View("Index");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserDao().GetByUsername(model.UserName);
                if (user.ConfirmStatus == true)
                {
                    var Result = new LoginDao().Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                    if (Result == 1)
                    {
                        var User = new UserDao().GetByUsername(model.UserName);
                        Session[Constants.USER_INFO] = User;
                        Session[Constants.WEBSITE_COLOR] = User.ColorWebsite;
                        var userSession = new UserLogin();
                        userSession.UserName = User.UserName;
                        userSession.UserID = User.ID;

                        Session[Constants.USER_SESSION] = userSession;
                        new LogDao().SetLog("Login", null, ((User)Session[Constants.USER_INFO]).ID);
                        //Session.Add(Constants.USER_SESSION, userSession);
                        if (Session[Constants.CURRENT_URL] != null)
                        {
                            return Redirect((string)Session[Constants.CURRENT_URL]);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else if (Result == 0)
                    {
                        ModelState.AddModelError("", "Không tồn tại tài khoản");
                    }
                    else if (Result == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản đang bị khoá");
                    }
                    else if (Result == -2)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng nhập không hợp lệ");
                    }
                }
                else
                {
                    return RedirectToAction("ConfirmCode", user);
                }
            }
            else
            {
                TempData["ValidationSumary"] = "Lỗi";
            }
            return View("Index");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userDao = new UserDao();
                if (userDao.CheckByUsername(model.Username))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (userDao.CheckByEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email bị trùng");
                }
                else
                {
                    var user = new User();
                    user.Name = model.Name;
                    user.UserName = model.Username;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.GroupID = Constants.GROUP_CLIENT;
                    user.ImagePath = "computer-icons-user-profile-login-my-account-icon-png-clip-art.png";
                    user.Status = true;

                    var code = new RandomString().MakeRandomString(7);
                    int lenght = 1;
                    for (int i = 0; i < lenght; i++)
                    {
                        if (userDao.CheckByCodeConfirm(code))
                        {
                            code = new RandomString().MakeRandomString(7);
                            lenght++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    user.CodeConfirm = code;
                    user.ConfirmStatus = false;

                    var result = userDao.Insert(user);
                    if (result > 0)
                    {
                        return RedirectToAction("ConfirmCode", user);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View(model);
        }

        public ActionResult ConfirmCode(User user)
        {
            if (user.CodeConfirm == null)
            {
                var code = new RandomString().MakeRandomString(7);
                int lenght = 1;
                for (int i = 0; i < lenght; i++)
                {
                    if (new UserDao().CheckByCodeConfirm(code))
                    {
                        code = new RandomString().MakeRandomString(7);
                        lenght++;
                    }
                    else
                    {
                        break;
                    }
                }
                user.CodeConfirm = code;
                new UserDao().Update(user);
            }
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/client/email_html/ConfirmCode.html"));
            content = content.Replace("{{Acc}}", user.UserName);
            content = content.Replace("{{CreateDate}}", user.CreateDate.ToString());
            content = content.Replace("{{CodeConfirm}}", user.CodeConfirm);

            var ToEmailAdmin = ConfigurationManager.AppSettings["ToEmail"].ToString();
            new MailHelper().SendMail(ToEmailAdmin, "EWBOK_Xác nhận mã", "Xác nhận mã đến từ EWBOK Bookstore", content);
            new MailHelper().SendMail(user.Email, "EWBOK_Xác nhận mã", "Xác nhận mã đến từ EWBOK Bookstore", content);
            TempData[Constants.ALERT_CONFIRMPASSWORD] = null;
            return View(user);
        }
        [HttpPost]
        public ActionResult ConfirmCode(string code, long id)
        {
            var user = new UserDao().Detail(id);
            if (code == user.CodeConfirm)
            {
                user.ConfirmStatus = true;
                new UserDao().Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                TempData[Constants.ALERT_CONFIRMPASSWORD] = "Sai mã xác nhận";
                return View("ConfirmCode", user);
            }
        }

        public ActionResult ForgotPassword()
        {
            TempData[Constants.ALERT_FORGOTPASSWORD_EMAIL] = null;
            TempData[Constants.ALERT_FORGOTPASSWORD_USERNAME] = null;
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email, string username)
        {
            var userdao = new UserDao();
            var user = userdao.GetByUsername(username);
            if (user != null)
            {
                if (user.Email == email)
                {
                    var newpass = new RandomString().MakeRandomString(7);
                    int lenght = 1;
                    for (int i = 0; i < lenght; i++)
                    {
                        if (userdao.CheckByPassword(newpass))
                        {
                            newpass = new RandomString().MakeRandomString(10);
                            lenght++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    user.Password = Encryptor.MD5Hash(newpass);

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/client/email_html/ForgotPassword.html"));
                    content = content.Replace("{{Acc}}", user.UserName);
                    content = content.Replace("{{CreateDate}}", user.CreateDate.ToString());
                    content = content.Replace("{{PasswordDefault}}", newpass);

                    var ToEmailAdmin = ConfigurationManager.AppSettings["ToEmail"].ToString();
                    new MailHelper().SendMail(ToEmailAdmin, "EWBOK_Quên mật khẩu", "Lấy mật khẩu mặc định đến từ EWBOK Bookstore, User ID : " + user.ID.ToString() + "Username : " + user.Name.ToString(), content);
                    new MailHelper().SendMail(user.Email, "EWBOK_Quên mật khẩu", "Lấy mật khẩu mặc định đến từ EWBOK Bookstore", content);

                    var result = userdao.Update(user);
                    if (result == true)
                    {
                        new LogDao().SetLog("Change Password", "Thành công", user.ID);
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    TempData[Constants.ALERT_FORGOTPASSWORD_EMAIL] = "Email không trùng khớp ";
                }
            }
            else
            {
                TempData[Constants.ALERT_FORGOTPASSWORD_USERNAME] = "Username không tồn tại";
            }
            return View("ForgotPassword");
        }
    }
}