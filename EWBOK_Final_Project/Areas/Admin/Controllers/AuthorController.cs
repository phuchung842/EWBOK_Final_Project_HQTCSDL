using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class AuthorController : BaseController
    {
        // GET: Admin/Author
        public ActionResult Index()
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_AUTHOR);
            var AuthorModel = new AuthorDao().ListAllAuthor();
            new LogDao().SetLog("Admin_Author_Index", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(AuthorModel);
        }
        public ActionResult Create()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_AUTHOR);
                new LogDao().SetLog("Admin_Author_Create", null, ((User)Session[Constants.USER_INFO]).ID);
                return View();
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Author_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Author");
        }
        [HttpPost]
        public ActionResult Create(Author author, HttpPostedFileBase postedFile)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (ModelState.IsValid)
                {
                    var userinfo = (User)Session[Constants.USER_INFO];
                    string path;
                    string filename = "";
                    string fullfilename = "";
                    if (postedFile == null)
                    {
                        fullfilename = "computer-icons-user-profile-login-my-account-icon-png-clip-art.png";
                        path = Path.Combine(Server.MapPath("~/Data/ImgAuthor"), fullfilename);
                        //postedFile.SaveAs(path);
                    }
                    else
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        filename = Path.GetFileName(postedFile.FileName);
                        fullfilename = filename.Split('.')[0] + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ")." + filename.Split('.')[1];
                        //Luu duong dan cua file
                        path = Path.Combine(Server.MapPath("~/Data/ImgAuthor"), fullfilename);
                        postedFile.SaveAs(path);
                    }

                    author.Image = fullfilename;
                    author.Status = true;
                    author.CreatedBy = userinfo.UserName;
                    author.MetaTitle = Unicode.RemoveUnicode(author.Name).Replace(" ", "-").ToLower().ToString();
                    author.Tag = Unicode.RemoveUnicode(author.Name).ToLower().ToString();
                    //author.CreatedBy = Session[Constants.USER_USERNAME].ToString();

                    long id = new AuthorDao().Insert(author);
                    if (id > 0)
                    {
                        SetAlert("Tạo Author thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Author_Create", "Tạo Author thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Author");
                    }
                    else
                    {
                        SetAlert("Tạo Author không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Author_Create", "Tạo Author không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Author");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    SetActiveSlideBar(Constants.SLIDEBAR_AD_AUTHOR);
                    new LogDao().SetLog("Admin_Author_Create", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Create",author);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Author_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Author");
        }

        public ActionResult Edit(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_AUTHOR);
                var AuthorModel = new AuthorDao().GetDetail(id);
                new LogDao().SetLog("Admin_Author_Edit", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(AuthorModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Author_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Author");
        }
        [HttpPost]
        public ActionResult Edit(Author author, HttpPostedFileBase postedFile)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (ModelState.IsValid)
                {
                    var userinfo = (User)Session[Constants.USER_INFO];
                    if (postedFile != null)
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        string filename = Path.GetFileName(postedFile.FileName);
                        string fullfilename = filename.Split('.')[0] + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ")." + filename.Split('.')[1];
                        //Luu duong dan cua file
                        string path = Path.Combine(Server.MapPath("~/Data/ImgAuthor"), fullfilename);
                        postedFile.SaveAs(path);

                        author.Image = fullfilename;
                    }
                    author.ModifiedBy = userinfo.UserName;
                    author.MetaTitle = Unicode.RemoveUnicode(author.Name).Replace(" ", "-").ToLower().ToString();
                    author.Tag = Unicode.RemoveUnicode(author.Name).ToLower().ToString();
                    //author.ModifiedBy = Session[Constants.USER_USERNAME].ToString();

                    var result = new AuthorDao().Update(author);

                    if (result)
                    {
                        //Cập nhật lại hình sau khi update
                        SetAlert("Thay đổi thông tin Author thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Author_Edit", "Thay đổi thông tin Author thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Author");
                    }
                    else
                    {
                        SetAlert("Thay đổi thông tin Author không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Author_Edit", "Thay đổi thông tin Author không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Author");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    SetActiveSlideBar(Constants.SLIDEBAR_AD_AUTHOR);
                    new LogDao().SetLog("Admin_Author_Edit", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Edit", author);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Author_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Author");
        }

        public ActionResult Delete(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_AUTHOR);
                var AuthorModel = new AuthorDao().GetDetail(id);
                new LogDao().SetLog("Admin_Author_Delete", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(AuthorModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Author_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Author");
        }
        [HttpPost]
        public ActionResult Delete(Author author)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                var setnull = new ProductDao().SetNullAuthor(author.ID);
                if (setnull)
                {
                    var result = new AuthorDao().Remove(author);
                    if (result)
                    {
                        SetAlert("Xoá Author thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Author_Delete", "Xoá Author thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Author");
                    }
                    else
                    {
                        SetAlert("Xoá Author không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Author_Delete", "Xoá Author không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Author");
                    }
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Author_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Author");
        }

        public ActionResult Detail(long id)
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_AUTHOR);
            var AuthorModel = new AuthorDao().GetDetail(id);
            new LogDao().SetLog("Admin_Author_Detail", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(AuthorModel);
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new AuthorDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}