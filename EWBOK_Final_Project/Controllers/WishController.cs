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
    public class WishController : BaseController
    {
        // GET: Wish
        public ActionResult Index()
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            var wish = new WishDao().ListAllWish((User)Session[Constants.USER_INFO]);
            List<Product> product = new List<Product>();
            for (int i = 0; i < wish.Count; i++)
            {
                var itemproduct = new ProductDao().GetDetail(wish[i].ProductID);
                product.Add(itemproduct);
            }
            new LogDao().SetLog("Wish", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(product);
        }
        public ActionResult AddItem(long id)
        {
            var product = new ProductDao().GetDetail(id);
            var wishresult = new WishDao().FindByUserIdAndProductID(((User)Session[Constants.USER_INFO]).ID, id);
            if (wishresult == false)
            {
                Wish wish = new Wish();
                wish.ProductID = id;
                wish.UserID = ((User)Session[Constants.USER_INFO]).ID;
                wish.CreateDate = DateTime.Now;
                wish.Status = 1;

                product.WishCount = product.WishCount + 1;
                var productDao = new ProductDao().Update(product);
                var result = new WishDao().Insert(wish);
                if (result)
                {
                    new LogDao().SetLog("Add Item Wish", "Thành công", ((User)Session[Constants.USER_INFO]).ID);
                    return Redirect((string)Session[Constants.CURRENT_URL]);
                }
                else
                {
                    new LogDao().SetLog("Add Item Wish", "Thất bại", ((User)Session[Constants.USER_INFO]).ID);
                    return Redirect((string)Session[Constants.CURRENT_URL]);
                }
            }
            new LogDao().SetLog("Add Item Wish", "Đã thêm" + product.Name.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            return Redirect((string)Session[Constants.CURRENT_URL]);
        }
        public ActionResult DeleteItem(long id)
        {
            var product = new ProductDao().GetDetail(id);
            var result = new WishDao().Delete(id, ((User)Session[Constants.USER_INFO]).ID);
            if(result)
            {
                product.WishCount--;
                new ProductDao().Update(product);
                new LogDao().SetLog("DeleteItem", "Thành công", ((User)Session[Constants.USER_INFO]).ID);
                return RedirectToAction("Index", "Wish");
            }
            else
            {
                new LogDao().SetLog("DeleteItem", "Thất bại", ((User)Session[Constants.USER_INFO]).ID);
                return RedirectToAction("Index", "Wish");
            }
        }
    }
}