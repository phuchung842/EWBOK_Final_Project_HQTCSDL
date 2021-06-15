using EWBOK_Final_Project.Common;
using EWBOK_Final_Project.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() 
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            var newproduct = new ProductDao().ListProductByNew();
            if (newproduct.Count >= 8)
            {
                ViewBag.NewProduct = new ProductDao().ListProductByNew(8);
            }
            else
            {
                ViewBag.NewProduct = new ProductDao().ListProductByNewTemp(8);
            }
            ViewBag.BestViewProduct = new ProductDao().ListProductByViewCount(8);
            ViewBag.DiscountProduct = new ProductDao().ListProductByDisCount(8);
            ViewBag.BestSellerProduct = new ProductDao().ListProductByBestSeller(8);
            ViewBag.WishProduct = new ProductDao().LisProductByWishCount(8);
            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Home", null, ((User)Session[Constants.USER_INFO]).ID);
            }
            return View();
        }

        [ChildActionOnly]
        public ActionResult Partial_Slide()
        {
            var slide = new SlideDao().ListAllActiveSlide();
            return PartialView(slide);
        }
        [ChildActionOnly]
        public ActionResult Partial_Testimonial()
        {
            ViewBag.Monitor = new UserDao().Detail(12);
            ViewBag.Member1 = new UserDao().Detail(13);
            ViewBag.Member2 = new UserDao().Detail(14);
            ViewBag.Member3 = new UserDao().Detail(15);
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Partial_Menu()
        {
            var cate = new ProductCategoryDao().ListAllProductCategoryActive().OrderBy(x => x.Name).ToList();
            var aut = new AuthorDao().ListAllAuthorActive().OrderBy(x => x.Name).ToList();
            ViewBag.Category = cate;
            ViewBag.Author = aut;
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Partial_Footer()
        {
            var about = new AboutDao().GetDetail(1);
            return PartialView(about);
        }
        [ChildActionOnly]
        public ActionResult Partial_Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult Partial_Header_Cart()
        {
            var cart = (List<CartItem>)Session[Constants.CART_SESSION];
            var listcart = new List<CartItem>();
            if (cart != null)
            {
                listcart = cart;
            }
            return PartialView(listcart);
        }
        [ChildActionOnly]
        public ActionResult Partial_Login()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Partial_QuickView(List<Product> model)
        {
            ViewBag.Cate = new ProductCategoryDao().ListAllProductCategory();
            return PartialView(model.ToList());
        }

        [ChildActionOnly]
        public ActionResult Partial_SideBarProduct()
        {
            ViewBag.Category = new ProductCategoryDao().ListAllProductCategoryActive();
            return PartialView();
        }
    }
}