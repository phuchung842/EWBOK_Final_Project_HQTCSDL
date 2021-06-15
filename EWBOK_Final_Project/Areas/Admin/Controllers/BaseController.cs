using EWBOK_Final_Project.Common;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[Constants.USER_SESSION];
            var userinfo = (User)Session[Constants.USER_INFO];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            if (userinfo != null)
            {
                if (userinfo.GroupID == Constants.GROUP_CLIENT.ToString())
                {
                    SetAlert("Tài khoản không có quyền đăng nhập", Constants.ALERTTYPE_ERROR);
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
                }
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "Success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "Warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "Error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

        protected void SetActiveSlideBar(string slidebar)
        {
            if (slidebar == Constants.SLIDEBAR_AD_HOME.ToString())
            {
                Session[Constants.SLIDEBAR_AD_HOME] = Constants.SLIDEBAR_ACTIVE;
                Session[Constants.SLIDEBAR_AD_RENT] = "";
                Session[Constants.SLIDEBAR_AD_USER] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCT] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = "";
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = "";
                Session[Constants.SLIDEBAR_AD_AUTHOR] = "";
                Session[Constants.SLIDEBAR_AD_PRICE] = "";
                Session[Constants.SLIDEBAR_AD_SYSTEM] = "";
            }
            else if (slidebar == Constants.SLIDEBAR_AD_RENT.ToString())
            {
                Session[Constants.SLIDEBAR_AD_HOME] = "";
                Session[Constants.SLIDEBAR_AD_RENT] = Constants.SLIDEBAR_ACTIVE;
                Session[Constants.SLIDEBAR_AD_USER] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCT] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = "";
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = "";
                Session[Constants.SLIDEBAR_AD_AUTHOR] = "";
                Session[Constants.SLIDEBAR_AD_PRICE] = "";
                Session[Constants.SLIDEBAR_AD_SYSTEM] = "";
            }
            else if (slidebar == Constants.SLIDEBAR_AD_USER.ToString())
            {
                Session[Constants.SLIDEBAR_AD_HOME] = "";
                Session[Constants.SLIDEBAR_AD_RENT] = "";
                Session[Constants.SLIDEBAR_AD_USER] = Constants.SLIDEBAR_ACTIVE;
                Session[Constants.SLIDEBAR_AD_PRODUCT] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = "";
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = "";
                Session[Constants.SLIDEBAR_AD_AUTHOR] = "";
                Session[Constants.SLIDEBAR_AD_PRICE] = "";
                Session[Constants.SLIDEBAR_AD_SYSTEM] = "";
            }
            else if (slidebar == Constants.SLIDEBAR_AD_PRODUCT.ToString())
            {
                Session[Constants.SLIDEBAR_AD_HOME] = "";
                Session[Constants.SLIDEBAR_AD_RENT] = "";
                Session[Constants.SLIDEBAR_AD_USER] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCT] = Constants.SLIDEBAR_ACTIVE;
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = "";
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = "";
                Session[Constants.SLIDEBAR_AD_AUTHOR] = "";
                Session[Constants.SLIDEBAR_AD_PRICE] = "";
                Session[Constants.SLIDEBAR_AD_SYSTEM] = "";
            }
            else if (slidebar == Constants.SLIDEBAR_AD_PRODUCTCATEGORY.ToString())
            {
                Session[Constants.SLIDEBAR_AD_HOME] = "";
                Session[Constants.SLIDEBAR_AD_RENT] = "";
                Session[Constants.SLIDEBAR_AD_USER] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCT] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = Constants.SLIDEBAR_ACTIVE;
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = "";
                Session[Constants.SLIDEBAR_AD_AUTHOR] = "";
                Session[Constants.SLIDEBAR_AD_PRICE] = "";
                Session[Constants.SLIDEBAR_AD_SYSTEM] = "";
            }
            else if (slidebar == Constants.SLIDEBAR_AD_AUTHOR.ToString())
            {
                Session[Constants.SLIDEBAR_AD_HOME] = "";
                Session[Constants.SLIDEBAR_AD_RENT] = "";
                Session[Constants.SLIDEBAR_AD_USER] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCT] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = "";
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = "";
                Session[Constants.SLIDEBAR_AD_AUTHOR] = Constants.SLIDEBAR_ACTIVE;
                Session[Constants.SLIDEBAR_AD_PRICE] = "";
                Session[Constants.SLIDEBAR_AD_SYSTEM] = "";
            }
            else if (slidebar == Constants.SLIDEBAR_AD_PUBLISHER.ToString())
            {
                Session[Constants.SLIDEBAR_AD_HOME] = "";
                Session[Constants.SLIDEBAR_AD_RENT] = "";
                Session[Constants.SLIDEBAR_AD_USER] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCT] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = "";
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = Constants.SLIDEBAR_ACTIVE;
                Session[Constants.SLIDEBAR_AD_AUTHOR] = "";
                Session[Constants.SLIDEBAR_AD_PRICE] = "";
                Session[Constants.SLIDEBAR_AD_SYSTEM] = "";
            }
            else if (slidebar == Constants.SLIDEBAR_AD_PRICE.ToString())
            {
                Session[Constants.SLIDEBAR_AD_HOME] = "";
                Session[Constants.SLIDEBAR_AD_RENT] = "";
                Session[Constants.SLIDEBAR_AD_USER] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCT] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = "";
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = "";
                Session[Constants.SLIDEBAR_AD_AUTHOR] = "";
                Session[Constants.SLIDEBAR_AD_PRICE] = Constants.SLIDEBAR_ACTIVE;
                Session[Constants.SLIDEBAR_AD_SYSTEM] = "";
            }
            else if (slidebar == Constants.SLIDEBAR_AD_SYSTEM.ToString())
            {
                Session[Constants.SLIDEBAR_AD_HOME] = "";
                Session[Constants.SLIDEBAR_AD_RENT] = "";
                Session[Constants.SLIDEBAR_AD_USER] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCT] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = "";
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = "";
                Session[Constants.SLIDEBAR_AD_AUTHOR] = "";
                Session[Constants.SLIDEBAR_AD_PRICE] = "";
                Session[Constants.SLIDEBAR_AD_SYSTEM] = Constants.SLIDEBAR_ACTIVE;
            }
            else
            {
                Session[Constants.SLIDEBAR_AD_HOME] = "";
                Session[Constants.SLIDEBAR_AD_RENT] = "";
                Session[Constants.SLIDEBAR_AD_USER] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCT] = "";
                Session[Constants.SLIDEBAR_AD_PRODUCTCATEGORY] = "";
                Session[Constants.SLIDEBAR_AD_PUBLISHER] = "";
                Session[Constants.SLIDEBAR_AD_AUTHOR] = "";
                Session[Constants.SLIDEBAR_AD_PRICE] = "";
                Session[Constants.SLIDEBAR_AD_SYSTEM] = "";
            }
        }
    }
}