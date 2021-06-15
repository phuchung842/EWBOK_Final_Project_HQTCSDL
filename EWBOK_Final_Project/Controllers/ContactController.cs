using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var about = new AboutDao().GetDetail(1);
            ViewBag.About = about;
            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Contact", null, ((User)Session[Constants.USER_INFO]).ID);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Feedback(Feedback feedback)
        {
            if(ModelState.IsValid)
            {
                feedback.ViewStatus = false;
                feedback.Status = true;
                var id = new FeedbackDao().Insert(feedback);
                if (id > 0)
                {
                    if (Session[Constants.USER_INFO] != null)
                    {
                        new LogDao().SetLog("Feedback", "Gửi feedback thành công", ((User)Session[Constants.USER_INFO]).ID);
                    }
                    return RedirectToAction("FeedbackSuccess", "Contact");
                }
                else
                {
                    if (Session[Constants.USER_INFO] != null)
                    {
                        new LogDao().SetLog("Feedback", "Gửi feedback không thành công", ((User)Session[Constants.USER_INFO]).ID);
                    }
                    return RedirectToAction("FeedbackError", "Contact");
                }
            }
            else
            {
                return View("Index", feedback);
            }
        }

        public ActionResult FeedbackSuccess()
        {
            return View();
        }
        public ActionResult FeedbackError()
        {
            return View();
        }
    }
}