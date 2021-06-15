using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class PublisherController : BaseController
    {
        // GET: Admin/Publisher
        public ActionResult Index()
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_PUBLISHER);
            var PublisherModel = new PublisherDao().ListAllPublisher();
            new LogDao().SetLog("Admin_Publisher_Index", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(PublisherModel);
        }
        public ActionResult Create()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_PUBLISHER);
                new LogDao().SetLog("Admin_Publisher_Create", null, ((User)Session[Constants.USER_INFO]).ID);
                return View();
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Publisher_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Publisher");
        }
        [HttpPost]
        public ActionResult Create(Publisher publisher)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (ModelState.IsValid)
                {
                    var userinfo = (User)Session[Constants.USER_INFO];
                    publisher.Status = true;
                    publisher.CreatedBy = userinfo.UserName;
                    publisher.MetaTitle = Unicode.RemoveUnicode(publisher.Name).Replace(" ", "-").ToLower().ToString();
                    publisher.Tag = Unicode.RemoveUnicode(publisher.Name).ToLower().ToString();
                    //publisher.CreatedBy = Session[Constants.USER_USERNAME].ToString();

                    long id = new PublisherDao().Insert(publisher);
                    if (id > 0)
                    {
                        SetAlert("Tạo Publisher thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Publisher_Create", "Tạo Publisher thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Publisher");
                    }
                    else
                    {
                        SetAlert("Tạo Publisher không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Publisher_Create", "Tạo Publisher không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Publisher");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    SetActiveSlideBar(Constants.SLIDEBAR_AD_PUBLISHER);
                    new LogDao().SetLog("Admin_Publisher_Create", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Create", publisher);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Publisher_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Publisher");
        }

        public ActionResult Edit(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_PUBLISHER);
                var PublisherModel = new PublisherDao().GetDetail(id);
                new LogDao().SetLog("Admin_Publisher_Edit", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(PublisherModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Publisher_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Publisher");
        }
        [HttpPost]
        public ActionResult Edit(Publisher publisher)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (ModelState.IsValid)
                {
                    var userinfo = (User)Session[Constants.USER_INFO];
                    publisher.ModifiedBy = userinfo.UserName;
                    publisher.MetaTitle = Unicode.RemoveUnicode(publisher.Name).Replace(" ", "-").ToLower().ToString();
                    publisher.Tag = Unicode.RemoveUnicode(publisher.Name).ToLower().ToString();
                    //publisher.ModifiedBy = Session[Constants.USER_USERNAME].ToString();

                    var result = new PublisherDao().Update(publisher);

                    if (result)
                    {
                        //Cập nhật lại hình sau khi update
                        SetAlert("Thay đổi thông tin Publisher thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Publisher_Edit", "Thay đổi thông tin Publisher thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Publisher");
                    }
                    else
                    {
                        SetAlert("Thay đổi thông tin Publisher không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Publisher_Edit", "Thay đổi thông tin Publisher không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Publisher");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    SetActiveSlideBar(Constants.SLIDEBAR_AD_PUBLISHER);
                    new LogDao().SetLog("Admin_Publisher_Edit", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Edit", publisher);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Publisher_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Publisher");
        }

        public ActionResult Delete(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_PUBLISHER);
                var PublisherModel = new PublisherDao().GetDetail(id);
                new LogDao().SetLog("Admin_Publisher_Delete", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(PublisherModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Publisher_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Publisher");
        }
        [HttpPost]
        public ActionResult Delete(Publisher publisher)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                var setnull = new ProductDao().SetNullPublisher(publisher.ID);
                if (setnull)
                {
                    var result = new PublisherDao().Remove(publisher);
                    if (result)
                    {
                        SetAlert("Xoá Publisher thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Publisher_Delete", "Xoá Publisher thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Publisher");
                    }
                    else
                    {
                        SetAlert("Xoá Publisher không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Publisher_Delete", "Xoá Publisher không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Publisher");
                    }
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Publisher_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Publisher");
        }

        public ActionResult Detail(long id)
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_PUBLISHER);
            var PublisherModel = new PublisherDao().GetDetail(id);
            new LogDao().SetLog("Admin_Publisher_Detail", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(PublisherModel);
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new PublisherDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}