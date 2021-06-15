using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using EWBOK_Final_Project.Common;
using Model.EF;

namespace EWBOK_Final_Project.Controllers
{
    public class ProductClientController : Controller
    {
        // GET: ProductClient
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Detail(long id)
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            var rentproduct = new RentProductDao().ListRentProductByProID(id);
            var product = new ProductDao().GetDetail(id);
            product.ViewCount++;
            new ProductDao().Update(product);
            ViewBag.Category = new ProductCategoryDao().ListAllProductCategory();
            ViewBag.Publisher = new PublisherDao().ListAllPublisher();
            ViewBag.Author = new AuthorDao().ListAllAuthor();
            ViewBag.RelatedProduct = new ProductDao().ListRelatedProduct(product);
            ViewBag.RentProduct = rentproduct;
            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Detail Product", "Mã sản phẩm : " + product.ID.ToString() + "Tên sản phẩm : " + product.Name.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            }
            return View(product);
        }
        public ActionResult Searching(string searchkey, int page = 1, int pageSize = 6)
        {
            if (searchkey == null)
            {
                searchkey = (string)Session[Constants.LISTPRODUCT_SEARCHKEY];
            }
            Session[Constants.LISTPRODUCT_SEARCHKEY] = searchkey;
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;

            //var author = new AuthorDao().ListAuthorBySearchKey(searchkey);
            //var publisher = new PublisherDao().ListPublisherBySearchKey(searchkey);
            //var productsearch = new ProductDao().ListProductBySearching(searchkey);
            //var productbyauthor = new ProductDao().ListProductByListAuthor(author);
            //var productbypublisher = new ProductDao().ListProductByListPublisher(publisher);
            //var listsearch = productsearch.Concat(productbyauthor).Concat(productbypublisher).ToList();
            //ProductComparer productComparer = new ProductComparer();
            //listsearch = listsearch.Distinct(productComparer).OrderByDescending(x => x.Name).ToList();
            var listsearch = new ProductDao().ListProductBySearching(searchkey);

            Session[Constants.CATEGORY_ACTIVE] = null;
            Session[Constants.CATE_ID] = Convert.ToInt64(0);
            Session[Constants.LISTPRODUCT] = listsearch;
            Session[Constants.LISTPRODUCT_VIEWNAME] = "Index";
            Session[Constants.LISTPRODUCT_ACTIONNAME] = "Searching";

            Session[Constants.MINPRICE] = Convert.ToDecimal(0);
            Session[Constants.MAXPRICE] = decimal.MaxValue;
            Session[Constants.SORT_ACTIVE] = 1;
            Session[Constants.SORTSTATUS_ACTIVE] = 1;

            Session[Constants.STATUSNAME_PRODUCT] = "Tìm kiếm : " + searchkey;

            int totalRecord = listsearch.Count();
            string link = "/ProductClient/Searching?searchkey=" + searchkey + "&";
            var listfinal = listsearch.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            SetPagination(totalRecord, pageSize, page, link);

            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("Searching", "Tìm kiếm : " + searchkey.ToString() + ", Trang : " + page.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            }

            return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listfinal);
        }
        public ActionResult ListAllProductByCategory(long id, int page = 1, int pageSize = 6)
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            Session[Constants.CATEGORY_ACTIVE] = new ProductCategoryDao().GetDetail(id);
            Session[Constants.CATE_ID] = Convert.ToInt64(id);
            var product = new ProductDao().ListAllProductActive();
            var productcate = new ProductDao().ListProductByCategory(id);
            Session[Constants.LISTPRODUCT] = product;
            Session[Constants.LISTPRODUCT_VIEWNAME] = "Index";
            Session[Constants.LISTPRODUCT_ACTIONNAME] = "ListAllProductByCategory";

            Session[Constants.MINPRICE] = Convert.ToDecimal(0);
            Session[Constants.MAXPRICE] = decimal.MaxValue;
            Session[Constants.SORT_ACTIVE] = 1;
            Session[Constants.SORTSTATUS_ACTIVE] = 1;

            Session[Constants.STATUSNAME_PRODUCT] = "";

            int totalRecord = productcate.Count();
            string link = "san-pham-theo-the-loai-" + id + "?";
            var listproduct = productcate.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            SetPagination(totalRecord, pageSize, page, link);

            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("ListAllProductByCategory", "Thể loại : " + id.ToString() + ", Trang : " + page.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            }

            return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listproduct);
        }
        public ActionResult ListAllProductByAuthor(long id, int page = 1, int pageSize = 6)
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            Session[Constants.CATEGORY_ACTIVE] = null;
            Session[Constants.CATE_ID] = Convert.ToInt64(0);
            Session[Constants.AUTHOR_ID] = id;
            var product = new ProductDao().ListProductbyAuthor(id);
            var author = new AuthorDao().GetDetail(id);
            Session[Constants.LISTPRODUCT] = product;
            Session[Constants.LISTPRODUCT_VIEWNAME] = "Index";
            Session[Constants.LISTPRODUCT_ACTIONNAME] = "ListAllProductByAuthor";

            Session[Constants.MINPRICE] = Convert.ToDecimal(0);
            Session[Constants.MAXPRICE] = decimal.MaxValue;
            Session[Constants.SORT_ACTIVE] = 1;
            Session[Constants.SORTSTATUS_ACTIVE] = 1;

            Session[Constants.STATUSNAME_PRODUCT] = "Tìm theo tác giả : " + author.Name;

            int totalRecord = product.Count();
            string link = "/san-pham-theo-tac-gia-" + id + "?";
            var listproduct = product.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            SetPagination(totalRecord, pageSize, page, link);

            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("ListAllProductByAuthor", "Tác giả : " + author.Name.ToString() + ", Trang : " + page.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            }

            return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listproduct);
        }
        public ActionResult ListAllProductByNew(int page = 1, int pageSize = 6)
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            Session[Constants.CATEGORY_ACTIVE] = null;
            Session[Constants.CATE_ID] = Convert.ToInt64(0);
            List<Product> product = new List<Product>();
            if (new ProductDao().ListProductByNew().Count() >= 8)
            {
                product = new ProductDao().ListProductByNew();
            }
            else
            {
                product = new ProductDao().ListProductByNewTemp();
            }
            Session[Constants.LISTPRODUCT] = product;
            Session[Constants.LISTPRODUCT_VIEWNAME] = "Index";
            Session[Constants.LISTPRODUCT_ACTIONNAME] = "ListAllProductByNew";

            Session[Constants.MINPRICE] = Convert.ToDecimal(0);
            Session[Constants.MAXPRICE] = decimal.MaxValue;
            Session[Constants.SORT_ACTIVE] = 1;
            Session[Constants.SORTSTATUS_ACTIVE] = 1;

            Session[Constants.STATUSNAME_PRODUCT] = "Mới";

            int totalRecord = product.Count();
            string link = "/san-pham-moi?";
            var listproduct = product.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            SetPagination(totalRecord, pageSize, page, link);

            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("ListAllProductByNew", "Trang : " + page.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            }

            return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listproduct);
        }
        public ActionResult ListAllProductByView(int page = 1, int pageSize = 6)
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            Session[Constants.CATEGORY_ACTIVE] = null;
            Session[Constants.CATE_ID] = Convert.ToInt64(0);
            var product = new ProductDao().ListProductByViewCount();
            Session[Constants.LISTPRODUCT] = product;
            Session[Constants.LISTPRODUCT_VIEWNAME] = "Index";
            Session[Constants.LISTPRODUCT_ACTIONNAME] = "ListAllProductByView";

            Session[Constants.MINPRICE] = Convert.ToDecimal(0);
            Session[Constants.MAXPRICE] = decimal.MaxValue;
            Session[Constants.SORT_ACTIVE] = 1;
            Session[Constants.SORTSTATUS_ACTIVE] = 1;

            Session[Constants.STATUSNAME_PRODUCT] = "Ưa chuộng";

            int totalRecord = product.Count();
            string link = "/san-pham-nhieu-luot-xem?";
            var listproduct = product.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            SetPagination(totalRecord, pageSize, page, link);

            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("ListAllProductByView", "Trang : " + page.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            }

            return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listproduct);
        }
        public ActionResult ListAllProductByWish(int page = 1, int pageSize = 6)
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            Session[Constants.CATEGORY_ACTIVE] = null;
            Session[Constants.CATE_ID] = Convert.ToInt64(0);
            var product = new ProductDao().LisProductByWishCount();
            Session[Constants.LISTPRODUCT] = product;
            Session[Constants.LISTPRODUCT_VIEWNAME] = "Index";
            Session[Constants.LISTPRODUCT_ACTIONNAME] = "ListAllProductByWish";

            Session[Constants.MINPRICE] = Convert.ToDecimal(0);
            Session[Constants.MAXPRICE] = decimal.MaxValue;
            Session[Constants.SORT_ACTIVE] = 1;
            Session[Constants.SORTSTATUS_ACTIVE] = 1;

            Session[Constants.STATUSNAME_PRODUCT] = "Ưa thích";

            int totalRecord = product.Count();
            string link = "/san-pham-nhieu-luot-thich?";
            var listproduct = product.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            SetPagination(totalRecord, pageSize, page, link);

            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("ListAllProductByWish", "Trang : " + page.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            }

            return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listproduct);
        }
        public ActionResult ListAllProductByDiscount(int page = 1, int pageSize = 6)
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            Session[Constants.CATEGORY_ACTIVE] = null;
            Session[Constants.CATE_ID] = Convert.ToInt64(0);
            var product = new ProductDao().ListProductByDisCount();
            Session[Constants.LISTPRODUCT] = product;
            Session[Constants.LISTPRODUCT_VIEWNAME] = "Index";
            Session[Constants.LISTPRODUCT_ACTIONNAME] = "ListAllProductByDiscount";

            Session[Constants.MINPRICE] = Convert.ToDecimal(0);
            Session[Constants.MAXPRICE] = decimal.MaxValue;
            Session[Constants.SORT_ACTIVE] = 1;
            Session[Constants.SORTSTATUS_ACTIVE] = 1;

            Session[Constants.STATUSNAME_PRODUCT] = "Giảm giá";

            int totalRecord = product.Count();
            string link = "/san-pham-dang-giam-gia?";
            var listproduct = product.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            SetPagination(totalRecord, pageSize, page, link);

            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("ListAllProductByDiscount", "Trang : " + page.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            }

            return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listproduct);
        }
        public ActionResult ListAllProductBySeller(int page = 1, int pageSize = 6)
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            Session[Constants.CATEGORY_ACTIVE] = null;
            Session[Constants.CATE_ID] = Convert.ToInt64(0);
            var product = new ProductDao().ListProductByBestSeller();
            Session[Constants.LISTPRODUCT] = product;
            Session[Constants.LISTPRODUCT_VIEWNAME] = "Index";
            Session[Constants.LISTPRODUCT_ACTIONNAME] = "ListAllProductBySeller";

            Session[Constants.MINPRICE] = Convert.ToDecimal(0);
            Session[Constants.MAXPRICE] = decimal.MaxValue;
            Session[Constants.SORT_ACTIVE] = 1;
            Session[Constants.SORTSTATUS_ACTIVE] = 1;

            Session[Constants.STATUSNAME_PRODUCT] = "Bán chạy";

            int totalRecord = product.Count();
            string link = "/ssan-pham-ban-chay?";
            var listproduct = product.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            SetPagination(totalRecord, pageSize, page, link);

            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("ListAllProductBySeller", "Trang : " + page.ToString(), ((User)Session[Constants.USER_INFO]).ID);
            }

            return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listproduct);
        }

        public ActionResult ListAllProductPost(decimal minprice = -1, decimal maxprice = decimal.MaxValue, long id = 0, int keysort = 0, int statussort = 0, int page = 1, int pageSize = 6)
        {
            Session[Constants.CURRENT_URL] = HttpContext.Request.RawUrl;
            string link = "";
            if (id > 0)
            {
                if (minprice == -1)
                {
                    minprice = (decimal)Session[Constants.MINPRICE];
                }
                if (maxprice == decimal.MaxValue)
                {
                    maxprice = (decimal)Session[Constants.MAXPRICE];
                }
                if (keysort == 0)
                {
                    keysort = (int)Session[Constants.SORT_ACTIVE];
                }
                if (statussort == 0)
                {
                    statussort = (int)Session[Constants.SORTSTATUS_ACTIVE];
                }
                link = HttpContext.Request.Url.AbsolutePath + "?minprice=" + minprice + "&maxprice=" + maxprice + "&keysort=" + keysort + "&statussort=" + statussort + "&";
            }
            else
            {
                bool flagmin = true;
                bool flagmax = true;
                bool flagkeysort = true;
                bool flagstatussort = true;
                if (minprice == -1)
                {
                    flagmin = false;
                    minprice = (decimal)Session[Constants.MINPRICE];
                }
                if (maxprice == decimal.MaxValue)
                {
                    flagmax = false;
                    maxprice = (decimal)Session[Constants.MAXPRICE];
                }
                if (keysort == 0)
                {
                    flagkeysort = false;
                    keysort = (int)Session[Constants.SORT_ACTIVE];
                }
                if (statussort == 0)
                {
                    flagstatussort = false;
                    statussort = (int)Session[Constants.SORTSTATUS_ACTIVE];
                }
                if (id == 0)
                {
                    id = (long)Session[Constants.CATE_ID];
                }

                if (flagmax == false || flagmin == false)
                {
                    link = HttpContext.Request.Url.AbsolutePath + "?keysort=" + keysort + "&statussort=" + statussort + "&";
                }
                if (flagkeysort == false || flagstatussort == false)
                {
                    link = HttpContext.Request.Url.AbsolutePath + "?minprice=" + minprice + "&maxprice=" + maxprice + "&";
                }
            }

            var product = (List<Product>)Session[Constants.LISTPRODUCT];
            if (product != null)
            {
                Session[Constants.MAXPRICE] = maxprice;
                Session[Constants.MINPRICE] = minprice;
                if (keysort > 1)
                {
                    Session[Constants.SORT_ACTIVE] = keysort;
                    Session[Constants.SORTSTATUS_ACTIVE] = statussort;
                    if (id > 0)
                    {
                        Session[Constants.CATE_ID] = id;
                        Session[Constants.CATEGORY_ACTIVE] = new ProductCategoryDao().GetDetail(id);
                        var filter = product.Where(x => x.Price >= minprice && x.Price <= maxprice && x.CategoryID == id).ToList();
                        int totalRecord = filter.Count();
                        filter = filter.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                        SetPagination(totalRecord, pageSize, page, link);

                        if(Session[Constants.USER_INFO]!=null)
                        {
                            new LogDao().SetLog("ListAllProductPost", null, ((User)Session[Constants.USER_INFO]).ID);
                        }

                        return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], Sort((int)Session[Constants.SORT_ACTIVE], statussort, filter));
                    }
                    else
                    {
                        var filter = product.Where(x => x.Price >= minprice && x.Price <= maxprice).ToList();
                        int totalRecord = filter.Count();
                        filter = filter.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                        SetPagination(totalRecord, pageSize, page, link);

                        if (Session[Constants.USER_INFO] != null)
                        {
                            new LogDao().SetLog("ListAllProductPost", null, ((User)Session[Constants.USER_INFO]).ID);
                        }

                        return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], Sort((int)Session[Constants.SORT_ACTIVE], statussort, filter));
                    }
                }
                else
                {
                    Session[Constants.SORT_ACTIVE] = keysort;
                    Session[Constants.SORTSTATUS_ACTIVE] = statussort;
                    if (id > 0)
                    {
                        Session[Constants.CATE_ID] = id;
                        Session[Constants.CATEGORY_ACTIVE] = new ProductCategoryDao().GetDetail(id);
                        var filter = product.Where(x => x.Price >= minprice && x.Price <= maxprice && x.CategoryID == id).ToList();
                        int totalRecord = filter.Count();
                        filter = filter.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                        SetPagination(totalRecord, pageSize, page, link);

                        if (Session[Constants.USER_INFO] != null)
                        {
                            new LogDao().SetLog("ListAllProductPost", null, ((User)Session[Constants.USER_INFO]).ID);
                        }

                        return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], filter);
                    }
                    else
                    {
                        var filter = product.Where(x => x.Price >= minprice && x.Price <= maxprice).ToList();
                        int totalRecord = filter.Count();
                        filter = filter.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                        SetPagination(totalRecord, pageSize, page, link);

                        if (Session[Constants.USER_INFO] != null)
                        {
                            new LogDao().SetLog("ListAllProductPost", null, ((User)Session[Constants.USER_INFO]).ID);
                        }

                        return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], filter);
                    }
                }
            }

            if (Session[Constants.USER_INFO] != null)
            {
                new LogDao().SetLog("ListAllProductPost", "Lỗi", ((User)Session[Constants.USER_INFO]).ID);
            }
            return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], product);
        }

        public List<Product> Sort(int sort, int statussort, List<Product> list)
        {
            //Sắp xếp tăng dần
            if (statussort == 2)
            {
                if (sort == 2)//sắp xếp theo tên
                {
                    //var listproduct = ((List<Product>)Session[Constants.LISTPRODUCT]).OrderBy(x => x.Name).ToList();
                    //Session[Constants.LISTPRODUCT] = listproduct;
                    return list.OrderBy(x => x.Name).ToList();
                }
                else if (sort == 3)//sắp xếp theo giá
                {
                    //var listproduct = ((List<Product>)Session[Constants.LISTPRODUCT]).OrderBy(x => x.Price).ToList();
                    //Session[Constants.LISTPRODUCT] = listproduct;
                    return list.OrderBy(x => x.Price).ToList();
                }
                else if (sort == 4)//sắp xếp theo ngày tạo
                {
                    //var listproduct = ((List<Product>)Session[Constants.LISTPRODUCT]).OrderBy(x => x.CreateDate).ToList();
                    //Session[Constants.LISTPRODUCT] = listproduct;
                    return list.OrderBy(x => x.CreateDate).ToList();
                }
                else
                {
                    //var listproduct = (List<Product>)Session[Constants.LISTPRODUCT];
                    //Session[Constants.LISTPRODUCT] = listproduct;
                    return list;
                }
            }
            //sắp xếp giảm dần
            else
            {
                if (sort == 2)//sắp xếp theo tên
                {
                    //var listproduct = ((List<Product>)Session[Constants.LISTPRODUCT]).OrderBy(x => x.Name).ToList();
                    //Session[Constants.LISTPRODUCT] = listproduct;
                    return list.OrderByDescending(x => x.Name).ToList();
                }
                else if (sort == 3)//sắp xếp theo giá
                {
                    //var listproduct = ((List<Product>)Session[Constants.LISTPRODUCT]).OrderBy(x => x.Price).ToList();
                    //Session[Constants.LISTPRODUCT] = listproduct;
                    return list.OrderByDescending(x => x.Price).ToList();
                }
                else if (sort == 4)//sắp xếp theo ngày tạo
                {
                    //var listproduct = ((List<Product>)Session[Constants.LISTPRODUCT]).OrderBy(x => x.CreateDate).ToList();
                    //Session[Constants.LISTPRODUCT] = listproduct;
                    return list.OrderByDescending(x => x.CreateDate).ToList();
                }
                else
                {
                    //var listproduct = (List<Product>)Session[Constants.LISTPRODUCT];
                    //Session[Constants.LISTPRODUCT] = listproduct;
                    return list;
                }
            }
        }

        private void SetPagination(int totalRecord, int pageSize, int page, string link)
        {
            Session[Constants.PAGE] = page;
            Session[Constants.PAGESIZE] = pageSize;
            Session[Constants.TOTALRECORD] = totalRecord;
            Session[Constants.LINK] = link;
        }



        //[HttpPost]
        //public ActionResult FilterByPrice(decimal? minprice = 0, decimal? maxprice = decimal.MaxValue)
        //{
        //    var listproduct = ((List<Product>)Session[Constants.LISTPRODUCT]).Where(x => x.Price >= minprice && x.Price <= maxprice).ToList();
        //    return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listproduct);
        //}
        //public ActionResult FilterByCategory(long id)
        //{
        //    var listproduct = ((List<Product>)Session[Constants.LISTPRODUCT]).Where(x => x.CategoryID == id).ToList();
        //    return View((string)Session[Constants.LISTPRODUCT_VIEWNAME], listproduct);
        //}
    }
}