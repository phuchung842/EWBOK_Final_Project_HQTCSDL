using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EWBOK_Final_Project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Checkout",
                url: "thanh-toan",
                defaults: new { controller = "Checkout", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "Wish",
                url: "yeu-thich",
                defaults: new { controller = "Wish", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "Home",
                url: "home",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "AddItem_Cart",
                url: "them-vao-gio-{id}",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "AddItem_Wish",
                url: "them-vao-yeu-thich-{id}",
                defaults: new { controller = "Wish", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "Category_Product",
                url: "san-pham-theo-the-loai-{id}",
                defaults: new { controller = "ProductClient", action = "ListAllProductByCategory", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "Author_Product",
                url: "san-pham-theo-tac-gia-{id}",
                defaults: new { controller = "ProductClient", action = "ListAllProductByAuthor", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "New_Product",
                url: "san-pham-moi",
                defaults: new { controller = "ProductClient", action = "ListAllProductByNew", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "BestView_Product",
                url: "san-pham-nhieu-luot-xem",
                defaults: new { controller = "ProductClient", action = "ListAllProductByView", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "BestWish_Product",
                url: "san-pham-nhieu-luot-thich",
                defaults: new { controller = "ProductClient", action = "ListAllProductByWish", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "Discount_Product",
                url: "san-pham-dang-giam-gia",
                defaults: new { controller = "ProductClient", action = "ListAllProductByDiscount", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "BestSeller_Product",
                url: "san-pham-ban-chay",
                defaults: new { controller = "ProductClient", action = "ListAllProductBySeller", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "Detail_Product",
                url: "{metatitle}-{id}",
                defaults: new { controller = "ProductClient", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "EWBOK_Final_Project.Controllers" }
            );
        }
    }
}
