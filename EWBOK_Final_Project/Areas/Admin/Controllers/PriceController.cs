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
    public class PriceController : BaseController
    {
        // GET: Admin/Price
        public ActionResult Index()
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_PRICE);
            var PriceModel = new PriceDao().ListAllPrice();
            new LogDao().SetLog("Admin_Price_Index", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(PriceModel);
        }
        
        public ActionResult Delete(long id)
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_PRICE);
            var PriceModel = new PriceDao().GetDetail(id);
            new LogDao().SetLog("Admin_Price_Delete", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(PriceModel);
        }
        [HttpPost]
        public ActionResult Delete(Price price)
        {
            var result = new PriceDao().Remove(price);
            if (result)
            {
                SetAlert("Xoá Price thành công", Constants.ALERTTYPE_SUCCESS);
                new LogDao().SetLog("Admin_Price_Delete", "Xoá Price thành công", ((User)Session[Constants.USER_INFO]).ID);
                return RedirectToAction("Index", "Price");
            }
            else
            {
                SetAlert("Xoá Price không thành công", Constants.ALERTTYPE_ERROR);
                new LogDao().SetLog("Admin_Price_Delete", "Xoá Price không thành công", ((User)Session[Constants.USER_INFO]).ID);
            }
            return View("Index");
        }

        public ActionResult Detail(long id)
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_PRICE);
            var PriceModel = new PriceDao().GetDetail(id);
            new LogDao().SetLog("Admin_Price_Detail", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(PriceModel);
        }
    }
}