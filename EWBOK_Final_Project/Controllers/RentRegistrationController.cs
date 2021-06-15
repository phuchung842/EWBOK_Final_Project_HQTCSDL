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
    public class RentRegistrationController : BaseController
    {
        // GET: RentRegistration
        public ActionResult Index()
        {
            var list = new RentRegistrationDao().ListAllRegistration(((User)Session[Constants.USER_INFO]).ID);
            ViewBag.ListProduct = new ProductDao().ListAllProductActive();
            return View(list);
        }
        public ActionResult Regis(long id, short quantity)
        {
            var result = new RentRegistrationDao().Regis(((User)Session[Constants.USER_INFO]).ID, quantity, id);
            if (result > 0)
            {
                return Redirect((string)Session[Constants.CURRENT_URL]);
            }
            else if(result==0)
            {
                return View();
            }
            else if(result==-1)
            {
                return View();
            }
            else 
            {
                return View();
            }
        }
        public ActionResult Cancel(long id, short quantity)
        {
            var result = new RentRegistrationDao().CancelRegis(quantity, id, ((User)Session[Constants.USER_INFO]).ID);
            if (result == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}