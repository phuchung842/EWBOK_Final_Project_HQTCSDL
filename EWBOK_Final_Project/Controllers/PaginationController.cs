using EWBOK_Final_Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Controllers
{
    public class PaginationController : Controller
    {
        // GET: Pagination
        private void Pagination(int totalRecord, int pageSize, int page, string link)
        {
            ViewBag.Link = link;
            ViewBag.Page = page;
            int maxPage = 5;
            //int totalPage = 0;
            int totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
        }
        public ActionResult Partial_Pagination()
        {
            int totalRecord = (int)Session[Constants.TOTALRECORD];
            int page = (int)Session[Constants.PAGE];
            int pageSize = (int)Session[Constants.PAGESIZE];
            string link = (string)Session[Constants.LINK];

            Pagination(totalRecord, pageSize, page, link);
            return PartialView();
        }
    }
}