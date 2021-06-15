using EWBOK_Final_Project.Common;
using EWBOK_Final_Project.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EWBOK_Final_Project.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = (List<CartItem>)Session[Constants.CART_SESSION];
            List<CartItem> listcart = new List<CartItem>();
            if (cart != null)
            {
                listcart = cart;
            }
            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Cart", null, ((User)Session[Constants.USER_INFO]).ID);
            }
            return View(listcart);
        }
        public ActionResult AddItem(long id, int quantity = 1)
        {
            var product = new ProductDao().GetDetail(id);
            var cart = (List<CartItem>)Session[Constants.CART_SESSION];
            if (cart == null)
            {
                CartItem item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                List<CartItem> listcart = new List<CartItem>();
                listcart.Add(item);
                Session[Constants.CART_SESSION] = listcart;
            }
            else
            {
                if (cart.Exists(x => x.Product.ID == id))
                {
                    for (int i = 0; i < cart.Count; i++)
                    {
                        if (cart[i].Product.ID == id)
                        {
                            cart[i].Quantity += quantity;
                        }
                    }
                    Session[Constants.CART_SESSION] = cart;
                }
                else
                {
                    CartItem item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    cart.Add(item);
                    Session[Constants.CART_SESSION] = cart;
                }
            }
            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Add Item Cart", null, ((User)Session[Constants.USER_INFO]).ID);
            }
            return Redirect((string)Session[Constants.CURRENT_URL]);
        }

        public JsonResult Update(string cartModel)
        {
            if (cartModel == null)
            {
                cartModel = (string)Session["test"];
            }
            Session["test"] = cartModel;
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var cart = (List<CartItem>)Session[Constants.CART_SESSION];
            for (int i = 0; i < cart.Count; i++)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == cart[i].Product.ID);
                if (jsonItem != null)
                {
                    cart[i].Quantity = jsonItem.Quantity;
                    if (Session[Constants.USER_INFO] != null)
                    {
                        new LogDao().SetLog("Update", "Cập nhật : " + cart[i].Product.Name.ToString() + ", Số lượng : " + cart[i].Quantity.ToString(), ((User)Session[Constants.USER_INFO]).ID);
                    }
                }
            }
            Session[Constants.CART_SESSION] = cart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult Delete(long id)
        {
            var cart = (List<CartItem>)Session[Constants.CART_SESSION];
            cart.RemoveAll(x => x.Product.ID == id);
            Session[Constants.CART_SESSION] = cart;
            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Delete Cart", null, ((User)Session[Constants.USER_INFO]).ID);
            }
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult DeleteAll()
        {
            Session[Constants.CART_SESSION] = null;
            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Delete all Cart", null, ((User)Session[Constants.USER_INFO]).ID);
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}