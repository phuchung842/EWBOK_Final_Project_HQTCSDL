using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EWBOK_Final_Project.Areas.Admin.Model;
using EWBOK_Final_Project.Common;
using Model.Dao;

namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[Constants.USER_SESSION] = null;
            Session[Constants.USER_INFO] = null;
            return View("Index");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var Result = new LoginDao().Login(model.UserName, Encryptor.MD5Hash(model.PassWord));

                if (Result == 1)
                {
                    var User = new UserDao().GetByUsername(model.UserName);
                    if (User.GroupID != Constants.GROUP_CLIENT.ToString())
                    {
                        Session[Constants.USER_INFO] = User;
                        var userSession = new UserLogin();
                        userSession.UserName = User.UserName;
                        userSession.UserID = User.ID;

                        Session[Constants.ERROR_ADMIN] = Constants.ERROR_ADMIN;
                        Session.Add(Constants.USER_SESSION, userSession);

                        new LogDao().SetLog("Admin_Login", null, User.ID);
                        return RedirectToAction("Index", "Home");
                    }
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
                TempData["ValidationSumary"] = "Lỗi";
            }
            return View("Index");
        }
    }
}