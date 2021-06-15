using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.sp_Models;


namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class RentController : BaseController
    {
        // GET: Admin/Rent
        public ActionResult Index()
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_RENT);
            var model = new RentingModel();
            model.ListReting = new RentDao().ListRenting();
            return View(model);
        }
        [HttpPost]
        public ActionResult Return(RentingModel model)
        {
            var listreturn = model.ListReting.Where(x => x.IsChecked == true).ToList();
            var result = new RentDao().ReturnRenting(listreturn);
            if (result == null)
            {
                SetAlert("Return Success", Constants.ALERTTYPE_SUCCESS);
            }
            else
            {
                SetAlert(result, Constants.ALERTTYPE_ERROR);
            }
            return RedirectToAction("Index");
        }
        public ActionResult WithRegis()
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_RENT);
            var regis = new RegisModel();
            regis.ListRegis = new RentRegistrationDao().ListAllRegistration();
            return View(regis);
        }
        [HttpPost]
        public ActionResult WithRegis(RegisModel model)
        {
            if (ModelState.IsValid)
            {
                var listrent = new List<RentItem>();
                var listregis = model.ListRegis.Where(x => x.IsChecked == true).ToList();

                for (int i = 0; i < listregis.Count; i++)
                {
                    var rentitem = new RentItem();
                    rentitem.ProductID = listregis[i].ProductID;
                    rentitem.DayRent = listregis[i].DayRent;
                    var product = new ProductDao().GetDetail(listregis[i].ProductID);
                    rentitem.RentMoney = product.Price.Value * (decimal)0.01 * listregis[i].DayRent;
                    rentitem.Deposit = product.Price.Value * (decimal)0.7;
                    listrent.Add(rentitem);
                }
                var result = new RentDao().RentWithRegis(listregis[0].UserID, listrent);
                if (result == null)
                {
                    SetAlert("Cho thuê thành công", Constants.ALERTTYPE_SUCCESS);
                    return RedirectToAction("WithRegis");
                }
                else
                {
                    SetAlert(result, Constants.ALERTTYPE_ERROR);
                    return RedirectToAction("WithRegis");
                }
            }
            else
            {
                model.ListRegis = new RentRegistrationDao().ListAllRegistration();
                return View("WithRegis",model);
            }
        }
        public ActionResult WithoutRegis()
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_RENT);
            var model = new ProductToCheckedModel();
            model.ListProductToCheckedModel = new RentDao().ListProductToChecked();
            return View(model);
        }
        [HttpPost]
        public ActionResult WithoutRegis(ProductToCheckedModel model, string Name, string IDNumber, string Address, string Email, string Phone)
        {
            if (ModelState.IsValid)
            {
                var listrent = new List<RentItemWithoutRegis>();
                var listregis = model.ListProductToCheckedModel.Where(x => x.IsChecked == true).ToList();
                for (int i = 0; i < listregis.Count; i++)
                {
                    var rentitem = new RentItemWithoutRegis();
                    rentitem.Item = new RentItem();
                    rentitem.Item.ProductID = listregis[i].ProductID;
                    rentitem.Item.DayRent = listregis[i].DayRent;
                    var product = new ProductDao().GetDetail(listregis[i].ProductID);
                    rentitem.Item.RentMoney = product.Price.Value * (decimal)0.01 * listregis[i].DayRent;
                    rentitem.Item.Deposit = product.Price.Value * (decimal)0.7;
                    rentitem.Quantity = listregis[i].QuantityChoose;
                    listrent.Add(rentitem);
                }
                var result = new RentDao().RentWithoutRegis(Name, IDNumber, Address, Email, Phone, listrent);
                if (result == null)
                {
                    SetAlert("Cho thuê thành công", Constants.ALERTTYPE_SUCCESS);
                    return RedirectToAction("WithRegis");
                }
                else
                {
                    SetAlert(result, Constants.ALERTTYPE_ERROR);
                    return RedirectToAction("WithRegis");
                }
            }
            else
            {
                model.ListProductToCheckedModel = new RentDao().ListProductToChecked();
                return View("WithoutRegis", model);
            }
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var list = (List<sp_ShowListProductToChecked>)Session[Constants.LISTPRODUCT_TOCHECKED];
            list.Where(x => x.ProductID == id).SingleOrDefault().IsChecked = !list.Where(x => x.ProductID == id).SingleOrDefault().IsChecked;
            var result = list.Where(x => x.ProductID == id).SingleOrDefault().IsChecked;
            Session[Constants.LISTPRODUCT_TOCHECKED] = list;
            return Json(new
            {
                status = result
            });
        }

    }
}