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
    public class RentController : BaseController
    {
        // GET: Rent
        public ActionResult Index()
        {
            var list = new RentDao().ListAllRentByUserID(((User)Session[Constants.USER_INFO]).ID);
            var listdetailofrent = new RentDao().ListDetailOfRentByUserID(((User)Session[Constants.USER_INFO]).ID);
            ViewBag.ListDetailOfRent = listdetailofrent;
            return View(list);
        }
    }
}