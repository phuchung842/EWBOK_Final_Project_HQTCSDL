using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWBOK_Final_Project.Common
{
    public class Constants
    {
        public static string USER_SESSION = "USER_SESSION";
        public static string USER_INFO = "USER_INFO";

        public static string GROUP_ADMIM = "ADMIN";
        public static string GROUP_CLIENT = "CLIENT";
        public static string GROUP_EMP = "EMP";

        public static string ALERTTYPE_SUCCESS = "Success";
        public static string ALERTTYPE_WARNING = "Warning";
        public static string ALERTTYPE_ERROR = "Error";
        public static string ALERT_CONFIRMPASSWORD = "ALERT_CONFIRMPASSWORD";
        public static string ALERT_FORGOTPASSWORD_USERNAME = "ALERT_FORGOTPASSWORD_USERNAME";
        public static string ALERT_FORGOTPASSWORD_EMAIL = "ALERT_FORGOTPASSWORD_EMAIL";

        public static string SLIDEBAR_ACTIVE = "active";
        public static string SLIDEBAR_AD_HOME = "HOME";
        public static string SLIDEBAR_AD_RENT = "RENT";
        public static string SLIDEBAR_AD_USER = "USER";
        public static string SLIDEBAR_AD_PRODUCT = "PRODUCT";
        public static string SLIDEBAR_AD_PRODUCTCATEGORY = "PRODUCTCATEGORY";
        public static string SLIDEBAR_AD_AUTHOR = "AUTHOR";
        public static string SLIDEBAR_AD_PUBLISHER = "PUBLISHER";
        public static string SLIDEBAR_AD_PRICE = "PRICE";
        public static string SLIDEBAR_AD_SYSTEM = "SYSTEM";   

        public static string LISTPRODUCT = "LISTPRODUCT";
        public static string LISTPRODUCT_VIEWNAME = "LISTPRODUCT_VIEWNAME";
        public static string LISTPRODUCT_ACTIONNAME = "LISTPRODUCT_ACTIONNAME";
        public static string LISTPRODUCT_SEARCHKEY = "LISTPRODUCT_SEARCHKEY";
        public static string LISTPRODUCT_TOCHECKED = "LISTPRODUCT_TOCHECKED";

        public static string MINPRICE = "MINPRICE";
        public static string MAXPRICE = "MAXPRICE";
        public static string CATEGORY_ACTIVE = "CATEGORY_ACTIVE";
        public static string CATE_ID = "CATE_ID";
        public static string AUTHOR_ID = "AUTHOR_ID";
        public static string SORT_ACTIVE = "SORT_ACTIVE";
        public static string SORTSTATUS_ACTIVE = "SORTSTATUS_ACTIVE";
        public static string STATUSNAME_PRODUCT = "STATUSNAME_PRODUCT";

        public static string PAGE = "PAGE";
        public static string PAGESIZE = "PAGESIZE";
        public static string TOTALRECORD = "TOTALRECORD";
        public static string LINK = "LINK";

        public static string CART_SESSION = "CART_SESSION";
        public static string CURRENT_URL = "CURRENT_URL";

        public static string WEBSITE_COLOR = "WEBSITE_COLOR";
        public static string WEBSITE_COLOR_ORANGE = "orange-theme.css";
        public static string WEBSITE_COLOR_GREEN = "green-theme.css";
        public static string WEBSITE_COLOR_BRIDGE = "bridge-theme.css";
        public static string WEBSITE_COLOR_DARKRED = "darkred-theme.css";
        public static string WEBSITE_COLOR_LITEBLUE = "liteblue-theme.css";
        public static string WEBSITE_COLOR_PINK = "pink-theme.css";
        public static string WEBSITE_COLOR_PURPLE = "purple-theme.css";
        public static string WEBSITE_COLOR_RED = "red-theme.css";
        public static string WEBSITE_COLOR_YELLOW = "yellow-theme.css";
        public static string WEBSITE_COLOR_ORCHID = "orchid-theme.css";
        public static string WEBSITE_COLOR_BLACK = "black-theme.css";
        public static string WEBSITE_COLOR_GRAY = "gray-theme.css";
        public static string WEBSITE_COLOR_BLUE = "blue-theme.css";

        public static string ERROR_ADMIN = "ERROR_ADMIN";
        //public static string CurrentCulture { set; get; }
    }
}