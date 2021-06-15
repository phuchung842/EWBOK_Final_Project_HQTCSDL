using EWBOK_Final_Project.Common;
using EWBOK_Final_Project.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Controllers
{
    public class CheckoutController : BaseController
    {
        // GET: Checkout
        public ActionResult Index()
        {
            var product = (List<CartItem>)Session[Constants.CART_SESSION];
            List<CartItem> listcart = new List<CartItem>();
            if(product!=null)
            {
                listcart = product;
            }
            new LogDao().SetLog("Check out", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(listcart);
        }
        [HttpPost]
        public ActionResult Payment(string receiver,string email,string phone, string address)
        {
            Order order = new Order();
            order.ShipName = receiver;
            order.ShipEmail = email;
            order.ShipMobile = phone;
            order.ShipAddress = address;
            order.CustomerID = ((User)Session[Constants.USER_INFO]).ID;
            order.CreateDate = DateTime.Now;
            order.Status = 1;
            try
            {
                var productDao = new ProductDao();
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[Constants.CART_SESSION];
                var detailDao = new OrderDetailDao();
                string listhtml = "";
                string row = "";
                decimal? total = 0;
                for (int i = 0; i < cart.Count; i++)
                {
                    var product = productDao.GetDetail(cart[i].Product.ID);
                    product.Quantity = product.Quantity - cart[i].Quantity;
                    product.SellerCount = product.SellerCount + cart[i].Quantity;
                    var orderdetail = new OrderDetail();
                    orderdetail.ProductID = cart[i].Product.ID;
                    orderdetail.OrderID = id;
                    orderdetail.Price = cart[i].Product.Price;
                    orderdetail.Quantity = cart[i].Quantity;
                    if (cart[i].Product.ProductStatus > 0)
                    {
                        orderdetail.PromotionPrice = cart[i].Product.PromotionPrice;
                        total = total + cart[i].Product.PromotionPrice * cart[i].Quantity;
                        row = "<tr><td>" + cart[i].Product.Name + "</td><td>" + cart[i].Product.Price + "</td><td>" + cart[i].Product.PromotionPrice + "</td><td>" + cart[i].Quantity + "</td><td>" + cart[i].Product.PromotionPrice * cart[i].Quantity + "</td></tr> ";
                    }
                    else
                    {
                        total = total + cart[i].Product.Price * cart[i].Quantity;
                        row = "<tr><td>" + cart[i].Product.Name + "</td><td>" + cart[i].Product.Price + "</td><td>" + cart[i].Product.PromotionPrice + "</td><td>" + cart[i].Quantity + "</td><td>" + cart[i].Product.Price * cart[i].Quantity + "</td></tr> ";
                    }
                    detailDao.Insert(orderdetail);
                    productDao.Update(product);
                    listhtml = listhtml + row;
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/client/email_html/Email_checkout.html"));
                content = content.Replace("{{CustomerName}}", receiver);
                content = content.Replace("{{Phone}}", phone);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString());
                content = content.Replace("{{OrderDetail}}", listhtml);

                var ToEmailAdmin = ConfigurationManager.AppSettings["ToEmail"].ToString();
                new MailHelper().SendMail(ToEmailAdmin, "EWBOK_Đơn hàng", "Đơn hàng đến từ EWBOK Bookstore", content);
                new MailHelper().SendMail(email, "EWBOK_Đơn hàng", "Đơn hàng đến từ EWBOK Bookstore", content);
            }
            catch
            {
                new LogDao().SetLog("Payment", "Thất bại", ((User)Session[Constants.USER_INFO]).ID);
                return View("PaymentError");
            }
            new LogDao().SetLog("Payment", "Thành công", ((User)Session[Constants.USER_INFO]).ID);
            return View("PaymentSuccess");
        }
    }
}