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
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var slide = new SlideDao().ListAllSilde();
                new LogDao().SetLog("Admin_Slide_Index", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(slide);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Slide_Index", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                new LogDao().SetLog("Admin_Slide_Create", null, ((User)Session[Constants.USER_INFO]).ID);
                return View();
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Slide_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase postedFile, Slide slide)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (ModelState.IsValid)
                {
                    var userinfo = (User)Session[Constants.USER_INFO];
                    string path;
                    string filename = "";
                    string fullfilename = "";
                    if (postedFile != null)
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        filename = Path.GetFileName(postedFile.FileName);
                        fullfilename = filename.Split('.')[0] + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ")." + filename.Split('.')[1];
                        //Luu duong dan cua file
                        path = Path.Combine(Server.MapPath("~/Data/ImgSlide"), fullfilename);
                        postedFile.SaveAs(path);
                    }

                    slide.Image = fullfilename;
                    slide.Status = true;
                    slide.CreatedBy = userinfo.UserName;

                    long id = new SlideDao().Insert(slide);
                    if (id > 0)
                    {
                        SetAlert("Tạo Slide thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Slide_Create", "Tạo Slide thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Slide");
                    }
                    else
                    {
                        SetAlert("Tạo Slide không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Slide_Create", "Tạo Slide không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Slide");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                    new LogDao().SetLog("Admin_Slide_Create", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Create", slide);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Slide_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var SlideModel = new SlideDao().GetDetail(id);
                new LogDao().SetLog("Admin_Slide_Edit", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(SlideModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Slide_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase postedFile, Slide slide)
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
                        string path = Path.Combine(Server.MapPath("~/Data/ImgSlide"), fullfilename);
                        postedFile.SaveAs(path);

                        slide.Image = fullfilename;
                    }
                    slide.ModifiedBy = userinfo.UserName;

                    var result = new SlideDao().Update(slide);

                    if (result)
                    {
                        //Cập nhật lại hình sau khi update
                        SetAlert("Thay đổi thông tin Slide thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Slide_Edit", "Thay đổi thông tin Slide thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Slide");
                    }
                    else
                    {
                        SetAlert("Thay đổi thông tin Slide không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Slide_Edit", "Thay đổi thông tin Slide không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Slide");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                    new LogDao().SetLog("Admin_Slide_Edit", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Edit", slide);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Slide_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Delete(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var SlideModel = new SlideDao().GetDetail(id);
                new LogDao().SetLog("Admin_Slide_Delete", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(SlideModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Slide_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Delete(Slide slide)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                var result = new SlideDao().Remove(slide);
                if (result)
                {
                    SetAlert("Xoá Slide thành công", Constants.ALERTTYPE_SUCCESS);
                    new LogDao().SetLog("Admin_Slide_Delete", "Xoá Slide thành công", ((User)Session[Constants.USER_INFO]).ID);
                    return RedirectToAction("Index", "Slide");
                }
                else
                {
                    SetAlert("Xoá Slide không thành công", Constants.ALERTTYPE_ERROR);
                    new LogDao().SetLog("Admin_Slide_Delete", "Xoá Slide không thành công", ((User)Session[Constants.USER_INFO]).ID);
                    return RedirectToAction("Index", "Slide");
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Slide_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Detail(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var SlideModel = new SlideDao().GetDetail(id);
                new LogDao().SetLog("Admin_Slide_Detail", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(SlideModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Slide_Detail", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new SlideDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}