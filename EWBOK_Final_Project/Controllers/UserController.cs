using EWBOK_Final_Project.Areas.Admin.Model;
using EWBOK_Final_Project.Common;
using EWBOK_Final_Project.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            var user = (User)Session[Constants.USER_INFO];
            ViewBag.ListPayment = new OrderDao().ListOrderByCustomerId(user.ID).Join(new OrderDetailDao().ListAllOrderDetail(), x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            ViewBag.ListProduct = new ProductDao().ListAllProduct();
            new LogDao().SetLog("User Info", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(user);
        }
        public ActionResult Edit()
        {
            new LogDao().SetLog("Edit User Info", null, ((User)Session[Constants.USER_INFO]).ID);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase postedFile, EditModel model)
        {
            var user = new UserDao().GetByUsername(model.Username);

            user.UserName = model.Username;
            user.Name = model.Name;
            user.Email = model.Email;
            user.Address = model.Address;
            user.Phone = model.Phone;
            if (postedFile != null)
            {
                //Luu ten fie, luu y bo sung thu vien using System.IO;
                string filename = Path.GetFileName(postedFile.FileName);
                string fullfilename = filename.Split('.')[0] + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ")." + filename.Split('.')[1];
                //Luu duong dan cua file
                string path = Path.Combine(Server.MapPath("~/Data/ImgAdmin"), fullfilename);
                postedFile.SaveAs(path);

                user.ImagePath = fullfilename;
            }
            try
            {
                new UserDao().Update(user);
                Session[Constants.USER_INFO] = user;
                new LogDao().SetLog("Edit User Info", "Thành công", ((User)Session[Constants.USER_INFO]).ID);
                return RedirectToAction("Index");
            }
            catch
            {
                new LogDao().SetLog("Edit User Info", "Thất bại", ((User)Session[Constants.USER_INFO]).ID);
                return RedirectToAction("Edit");
            }
        }

        public ActionResult ChangePassword()
        {
            new LogDao().SetLog("Change Password User Info", null, ((User)Session[Constants.USER_INFO]).ID);
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var currentuser = (User)Session[Constants.USER_INFO];
            if (ModelState.IsValid)
            {
                if (currentuser.Password == Encryptor.MD5Hash(model.Password))
                {
                    currentuser.Password = Encryptor.MD5Hash(model.NewPassword);
                    try
                    {
                        new UserDao().Update(currentuser);
                        new LogDao().SetLog("Change Password User Info", "Thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        new LogDao().SetLog("Change Password User Info", "Thất bại", ((User)Session[Constants.USER_INFO]).ID);
                        return View();
                    }
                }
                else
                {
                    new LogDao().SetLog("Change Password User Info", "Không đúng với mật khẩu hiện taị", ((User)Session[Constants.USER_INFO]).ID);
                    ModelState.AddModelError("", "Không đúng với mật khẩu hiện taị");
                }
            }
            return View();
        }

        public ActionResult ChangeColorWebsite(string color)
        {
            var user = (User)Session[Constants.USER_INFO];
            if (color == "red")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_RED.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_RED.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Đỏ thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "black")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_BLACK.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_BLACK.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Đen thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "blue")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_BLUE.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_BLUE.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Xanh dương thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "bridge")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_BRIDGE.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_BRIDGE.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Bridge thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "darkred")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_DARKRED.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_DARKRED.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Đỏ đô thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "gray")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_GRAY.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_GRAY.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Xám thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "green")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_GREEN.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_GREEN.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Xanh lá thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "liteblue")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_LITEBLUE.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_LITEBLUE.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "LiteBlue thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "orange")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_ORANGE.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_ORANGE.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Cam thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "orchid")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_ORCHID.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_ORCHID.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Orchid thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "pink")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_PINK.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_PINK.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Hồng thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "purple")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_PURPLE.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_PURPLE.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Tím thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else if (color == "yellow")
            {
                Session[Constants.WEBSITE_COLOR] = Constants.WEBSITE_COLOR_YELLOW.ToString();
                user.ColorWebsite = Constants.WEBSITE_COLOR_YELLOW.ToString();
                new UserDao().Update(user);
                new LogDao().SetLog("Change Color Website", "Vàng thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            else
            {
                new LogDao().SetLog("Change Color Website", "Thất bại", ((User)Session[Constants.USER_INFO]).ID);
            }
            return RedirectToAction("Index");
        }

    }
}