using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_USER);
                var UserModel = new UserDao().ListAll();
                new LogDao().SetLog("Admin_User_Index", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(UserModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_User_Index", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_USER);
                new LogDao().SetLog("Admin_User_Create", null, ((User)Session[Constants.USER_INFO]).ID);
                return View();
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_User_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Create(User user, HttpPostedFileBase postedFile)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (ModelState.IsValid)
                {
                    if (new UserDao().CheckByUsername(user.UserName) == false)
                    {
                        if (new UserDao().CheckByEmail(user.Email) == false)
                        {
                            string path;
                            string filename = "";
                            string fullfilename = "";
                            var userinfo = (User)Session[Constants.USER_INFO];
                            if (postedFile == null)
                            {
                                fullfilename = "computer-icons-user-profile-login-my-account-icon-png-clip-art.png";
                                path = Path.Combine(Server.MapPath("~/Data/ImgAdmin"), fullfilename);
                                //postedFile.SaveAs(path);
                            }
                            else
                            {

                                //Luu ten fie, luu y bo sung thu vien using System.IO;
                                filename = Path.GetFileName(postedFile.FileName);
                                fullfilename = filename.Split('.')[0] + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ")." + filename.Split('.')[1];
                                //Luu duong dan cua file
                                path = Path.Combine(Server.MapPath("~/Data/ImgAdmin"), fullfilename);
                                postedFile.SaveAs(path);
                            }
                            if (string.IsNullOrEmpty(user.Password))
                            {
                                user.Password = Encryptor.MD5Hash("12345");
                            }
                            else
                            {
                                user.Password = Encryptor.MD5Hash(user.Password);
                            }
                            user.ImagePath = fullfilename;
                            user.Status = true;
                            user.ConfirmStatus = true;
                            user.CreatedBy = userinfo.UserName;
                            //user.CreatedBy = Session[Constants.USER_USERNAME].ToString();

                            long id = new UserDao().Insert(user);

                            if (id > 0)
                            {
                                SetAlert("Tạo User thành công", Constants.ALERTTYPE_SUCCESS);
                                new LogDao().SetLog("Admin_User_Create", "Tạo User thành công", ((User)Session[Constants.USER_INFO]).ID);
                                return RedirectToAction("Index", "User");
                            }
                            else
                            {
                                SetAlert("Tạo User không thành công", Constants.ALERTTYPE_ERROR);
                                new LogDao().SetLog("Admin_User_Create", "Tạo User không thành công", ((User)Session[Constants.USER_INFO]).ID);
                                return RedirectToAction("Index", "User");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Email đã tồn tại");
                            new LogDao().SetLog("Admin_User_Create", "Email đã tồn tại", ((User)Session[Constants.USER_INFO]).ID);
                            return View("Create", user);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username đã tồn tại");
                        new LogDao().SetLog("Admin_User_Create", "Username đã tồn tại", ((User)Session[Constants.USER_INFO]).ID);
                        return View("Create", user);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không hợp lệ");
                    new LogDao().SetLog("Admin_User_Create", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Create", user);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_User_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_USER);
                var UserModel = new UserDao().Detail(id);
                new LogDao().SetLog("Admin_User_Edit", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(UserModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_User_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Edit(User user, HttpPostedFileBase postedFile)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                bool flag = true;
                if (user.Password == null)
                {
                    var tempuser = new UserDao().GetByUsername(user.UserName);
                    user.Password = tempuser.Password;
                    flag = false;
                }
                if (ModelState.IsValid)
                {
                    if (new UserDao().CheckByEmailInEdit(user.Email) == false)
                    {
                        var userinfo = (User)Session[Constants.USER_INFO];
                        if (flag == true)
                        {
                            if (!string.IsNullOrEmpty(user.Password))
                            {
                                user.Password = Encryptor.MD5Hash(user.Password);
                            }
                        }
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
                        user.ModifiedBy = userinfo.UserName;
                        //user.ModifiedBy = Session[Constants.USER_USERNAME].ToString();
                        var result = new UserDao().Update(user);
                        if (result)
                        {
                            //Cập nhật lại hình sau khi update
                            if (user.ID.ToString() == user.ID.ToString())
                            {
                                if (user.ImagePath != null)
                                {
                                    userinfo.ImagePath = user.ImagePath;
                                }
                            }
                            SetAlert("Thay đổi thông tin User thành công", Constants.ALERTTYPE_SUCCESS);
                            new LogDao().SetLog("Admin_User_Edit", "Thay đổi thông tin User thành công", ((User)Session[Constants.USER_INFO]).ID);
                            return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            SetAlert("Thay đổi thông tin User không thành công", Constants.ALERTTYPE_ERROR);
                            new LogDao().SetLog("Admin_User_Edit", "Thay đổi thông tin User không thành công", ((User)Session[Constants.USER_INFO]).ID);
                            return RedirectToAction("Index", "User");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email đã tồn tại");
                        new LogDao().SetLog("Admin_User_Edit", "Email đã tồn tại", ((User)Session[Constants.USER_INFO]).ID);
                        return View("Edit", user);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    new LogDao().SetLog("Admin_User_Edit", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Edit", user);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_User_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Index", "User");
        }
        public ActionResult Delete(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_USER);
                var UserModel = new UserDao().Detail(id);
                new LogDao().SetLog("Admin_User_Delete", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(UserModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_User_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Delete(User user)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                var result = new UserDao().Remove(user);
                if (result)
                {
                    SetAlert("Xoá User thành công", Constants.ALERTTYPE_SUCCESS);
                    new LogDao().SetLog("Admin_User_Delete", "Xoá User thành công", ((User)Session[Constants.USER_INFO]).ID);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Xoá User không thành công", Constants.ALERTTYPE_ERROR);
                    new LogDao().SetLog("Admin_User_Delete", "Xoá User không thành công", ((User)Session[Constants.USER_INFO]).ID);
                    return RedirectToAction("Index", "User");
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_User_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Detail(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_USER);
                var UserModel = new UserDao().Detail(id);
                new LogDao().SetLog("Admin_User_Detail", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(UserModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_User_Detail", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ChangeStatus(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                var result = new UserDao().ChangeStatus(id);
                return Json(new
                {
                    status = result
                });
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            return RedirectToAction("Index", "Home");
        }
    }
}