using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class FeedbackController : BaseController
    {
        // GET: Admin/Feedback
        public ActionResult Index()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var feedback = new FeedbackDao().ListAllFeedback();
                new LogDao().SetLog("Admin_Feedback_Index", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(feedback);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Feedback_Index", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Detail(int id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var FeedbackModel = new FeedbackDao().GetDetail(id);
                new LogDao().SetLog("Admin_Feedback_Detail", null, ((User)Session[Constants.USER_INFO]).ID);
                FeedbackModel.ViewStatus = true;
                new FeedbackDao().Update(FeedbackModel);
                return View(FeedbackModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Feedback_Detail", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Delete(int id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_SYSTEM);
                var FeedbackModel = new FeedbackDao().GetDetail(id);
                new LogDao().SetLog("Admin_Feedback_Delete", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(FeedbackModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Feedback_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Delete(Feedback feedback)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                var result = new FeedbackDao().Remove(feedback);
                if (result)
                {
                    SetAlert("Xoá Feedback thành công", Constants.ALERTTYPE_SUCCESS);
                    new LogDao().SetLog("Admin_Feedback_Delete", "Xoá Feedback thành công", ((User)Session[Constants.USER_INFO]).ID);
                    return RedirectToAction("Index", "Feedback");
                }
                else
                {
                    SetAlert("Xoá Feedback không thành công", Constants.ALERTTYPE_ERROR);
                    new LogDao().SetLog("Admin_Feedback_Delete", "Xoá Feedback không thành công", ((User)Session[Constants.USER_INFO]).ID);
                    return RedirectToAction("Index", "Feedback");
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Feedback_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Home");
        }
    }
}