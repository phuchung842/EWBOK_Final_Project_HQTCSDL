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
    public class AboutController : BaseController
    {
        // GET: Admin/About
        public ActionResult Index()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var about = new AboutDao().ListAllAbout();
                new LogDao().SetLog("Admin_About_Index", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(about);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_About_Index", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var AboutModel = new AboutDao().GetDetail(id);
                new LogDao().SetLog("Admin_About_Edit", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(AboutModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_About_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Edit(About about)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (ModelState.IsValid)
                {
                    var userinfo = (User)Session[Constants.USER_INFO];
                    about.ModifiedBy = userinfo.UserName;

                    var result = new AboutDao().Update(about);

                    if (result)
                    {
                        //Cập nhật lại hình sau khi update
                        SetAlert("Thay đổi thông tin About thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_About_Edit", "Thay đổi thông tin About thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "About");
                    }
                    else
                    {
                        SetAlert("Thay đổi thông tin About không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_About_Edit", "Thay đổi thông tin About không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "About");
                    }
                }
                else
                {
                    new LogDao().SetLog("Admin_About_Edit", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    return View("Edit", about);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_About_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Detail(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var AboutModel = new AboutDao().GetDetail(id);
                new LogDao().SetLog("Admin_About_Detail", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(AboutModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_About_Detail", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
    }
}