using EWBOK_Final_Project.Areas.Admin.Model;
using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_HOME);
            var orderDao = new OrderDao();
            var productDao = new ProductDao();
            var productcateDao = new ProductCategoryDao();
            var orderdetail = new OrderDetailDao().ListAllOrderDetail();

            //statistic
            ViewBag.TotalUser = new UserDao().ListAll().Count;
            ViewBag.TotalView = new ProductDao().ListAllProductActive().Count;
            ViewBag.TotalProductBellow50 = new ProductDao().ListAllProductActive().Where(x => x.Quantity <= 50).ToList().Count;
            ViewBag.TotalPurchased = new ProductDao().ListAllProductActive().Sum(x => x.SellerCount);
            //statistic

            //piechart
            var IncomeTemp = productDao.ListAllProduct().Join(orderdetail, x => x.ID, y => y.ProductID, (p, o) => new JoinProduct_OrderDetail { Product = p, OrderDetail = o }).ToList();
            var AllCategory = productcateDao.ListAllProductCategoryActive();
            string CateName = "";
            string CateColor = "";
            string CateIncome = "";
            var IncomeEachCategory = IncomeTemp.GroupBy(x => x.Product.CategoryID).ToList();
            decimal[] SumByCate = new decimal[AllCategory.Count];

            for (int i = 0; i < IncomeEachCategory.Count; i++)
            {
                int pos = -1;
                for (int j = 0; j < AllCategory.Count; j++)
                {
                    if (IncomeEachCategory[i].Key == AllCategory[j].ID)
                    {
                        pos = j;
                        break;
                    }
                }
                SumByCate[pos] = IncomeEachCategory[i].Sum(x => x.OrderDetail.PromotionPrice.HasValue ? x.OrderDetail.PromotionPrice.Value : x.OrderDetail.Price.Value);
            }
            for (int i = 0; i < AllCategory.Count; i++)
            {
                if (i + 1 == AllCategory.Count)
                {
                    CateName += "'" + AllCategory[i].Name + "'";
                    CateColor += "'" + AllCategory[i].Color + "'";
                    CateIncome += SumByCate[i].ToString();
                }
                else
                {
                    CateName += "'" + AllCategory[i].Name + "'" + ",";
                    CateColor += "'" + AllCategory[i].Color + "'" + ",";
                    CateIncome += SumByCate[i].ToString() + ",";
                }
            }
            ViewBag.CateName = CateName;
            ViewBag.CateColor = CateColor;
            ViewBag.CateIncome = CateIncome;
            //piechart

            //data
            var dataM1 = orderDao.ListByMonthAndYear(1, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM2 = orderDao.ListByMonthAndYear(2, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM3 = orderDao.ListByMonthAndYear(3, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM4 = orderDao.ListByMonthAndYear(4, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM5 = orderDao.ListByMonthAndYear(5, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM6 = orderDao.ListByMonthAndYear(6, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM7 = orderDao.ListByMonthAndYear(7, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM8 = orderDao.ListByMonthAndYear(8, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM9 = orderDao.ListByMonthAndYear(9, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM10 = orderDao.ListByMonthAndYear(10, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM11 = orderDao.ListByMonthAndYear(11, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            var dataM12 = orderDao.ListByMonthAndYear(12, 2020).Join(orderdetail, x => x.ID, y => y.OrderID, (o, d) => new JoinOrder_OrderDetail { Order = o, OrderDetail = d }).ToList();
            //data

            //linechart
            ViewBag.CountM1 = dataM1;
            ViewBag.CountM2 = dataM2;
            ViewBag.CountM3 = dataM3;
            ViewBag.CountM4 = dataM4;
            ViewBag.CountM5 = dataM5;
            ViewBag.CountM6 = dataM6;
            ViewBag.CountM7 = dataM7;
            ViewBag.CountM8 = dataM8;
            ViewBag.CountM9 = dataM9;
            ViewBag.CountM10 =dataM10;
            ViewBag.CountM11 =dataM11;
            ViewBag.CountM12 =dataM12;
            //linchart

            //barchart
            ViewBag.IncomeM1 = dataM1;
            ViewBag.IncomeM2 = dataM2;
            ViewBag.IncomeM3 = dataM3;
            ViewBag.IncomeM4 = dataM4;
            ViewBag.IncomeM5 = dataM5;
            ViewBag.IncomeM6 = dataM6;
            ViewBag.IncomeM7 = dataM7;
            ViewBag.IncomeM8 = dataM8;
            ViewBag.IncomeM9 = dataM9;
            ViewBag.IncomeM10 =dataM10;
            ViewBag.IncomeM11 =dataM11;
            ViewBag.IncomeM12 =dataM12;
            //barchart

            //table order
            var order = orderDao.ListAllOrder();
            var user = new UserDao().ListAll();
            var tableorder = order.Join(user, x => x.CustomerID, y => y.ID, (o, u) => new JoinOrder_User { Order = o, User = u }).OrderBy(x=>x.Order.CreateDate).ToList();
            //table order

            new LogDao().SetLog("Admin_Home_Index", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(tableorder);
        }
        public ActionResult DetailOrder(long id)
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_HOME);
            var orderdetail = new OrderDetailDao().ListOrderDetailById(id);
            var order = new OrderDao().GetOrderById(id);
            var user = new UserDao().Detail(order.CustomerID.Value);
            var product = new ProductDao().ListAllProduct();
            var tableorderdetail = orderdetail.Join(product, x => x.ProductID, y => y.ID, (d, p) => new JoinProduct_OrderDetail { OrderDetail = d, Product = p }).ToList();
            ViewBag.Order = order;
            ViewBag.User = user;
            new LogDao().SetLog("Admin_Home_DetailOrder", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(tableorderdetail);
        }
        public ActionResult Partial_Notification()
        {
            var feedback = new FeedbackDao().ListFeedbackByViewStatusFalse();
            return PartialView(feedback);
        }
    }
}