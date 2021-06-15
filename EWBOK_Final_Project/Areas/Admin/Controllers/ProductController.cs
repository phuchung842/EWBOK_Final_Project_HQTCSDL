using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCT);
            var ProductModel = new ProductDao().ListAllProduct();
            new LogDao().SetLog("Admin_Product_Index", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(ProductModel);
        }

        public ActionResult Create()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCT);
                ViewBag.ListPublisher = new PublisherDao().ListAllPublisherActive();
                ViewBag.ListAuthor = new AuthorDao().ListAllAuthorActive();
                ViewBag.ListCategory = new ProductCategoryDao().ListAllProductCategoryActive();
                new LogDao().SetLog("Admin_Product_Create", null, ((User)Session[Constants.USER_INFO]).ID);
                return View();
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Product_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Product");
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase postedFile)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (postedFile != null)
                {
                    product.Image = "temp";
                }
                if (ModelState.IsValid)
                {
                    string path;
                    string filename = "";
                    string fullfilename = "";
                    var userinfo = (User)Session[Constants.USER_INFO];
                    if (postedFile == null)
                    {
                        filename = "computer-icons-user-profile-login-my-account-icon-png-clip-art.png"; //  lấy 1 file mặc định không thêm ảnh"
                        fullfilename = filename.Split('.')[0] + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ")." + filename.Split('.')[1];
                        //postedFile.SaveAs(path);
                    }
                    else
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        filename = Path.GetFileName(postedFile.FileName);
                        fullfilename = filename.Split('.')[0] + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ")." + filename.Split('.')[1];
                        //Luu duong dan cua file
                        path = Path.Combine(Server.MapPath("~/Data/ImgProduct"), fullfilename);
                        postedFile.SaveAs(path);
                    }
                    if (product.ProductStatus > 0)
                    {
                        if (!string.IsNullOrEmpty(product.ProductStatus.ToString()))
                        {
                            product.PromotionPrice = product.Price - product.Price * (product.ProductStatus / 100);
                        }
                    }

                    var cate = new ProductCategoryDao().GetDetail((long)product.CategoryID);
                    product.Code = cate.MetaTitle.ToString() + "_" + product.ID.ToString();
                    product.Image = fullfilename;
                    product.Status = true;
                    product.CreatedBy = userinfo.UserName;
                    product.MetaTitle = Unicode.RemoveUnicode(product.Name).Replace(" ", "-").Replace("&", "va").Replace(":","la").ToLower().ToString();
                    product.Tag = Unicode.RemoveUnicode(product.Name).ToLower().ToString();

                    long id = new ProductDao().Insert(product);
                    if (id > 0)
                    {
                        //Begin (Tạo giá)
                        var pricemodel = new PriceDao();
                        pricemodel.SetStatusFalse(id);
                        var price = new Price();
                        price.PriceValue = product.Price;
                        price.PromotionPrice = product.PromotionPrice;
                        price.Name = product.Name;
                        price.ProductID = product.ID;
                        product.CreatedBy = userinfo.UserName;
                        //price.CreatedBy = Session[Constants.USER_USERNAME].ToString();

                        pricemodel.Insert(price);
                        //End (Tạo giá)

                        SetAlert("Tạo Product thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Product_Create", "Tạo Product thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        SetAlert("Tạo Product không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Product_Create", "Tạo Product không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    new LogDao().SetLog("Admin_Product_Create", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCT);
                    ViewBag.ListPublisher = new PublisherDao().ListAllPublisherActive();
                    ViewBag.ListAuthor = new AuthorDao().ListAllAuthorActive();
                    ViewBag.ListCategory = new ProductCategoryDao().ListAllProductCategoryActive();
                    return View("Create", product);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Product_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Edit(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCT);
                var ProductModel = new ProductDao().GetDetail(id);
                ViewBag.ListPublisher = new PublisherDao().ListAllPublisherActive();
                ViewBag.ListAuthor = new AuthorDao().ListAllAuthorActive();
                ViewBag.ListCategory = new ProductCategoryDao().ListAllProductCategoryActive();
                new LogDao().SetLog("Admin_Product_Edit", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(ProductModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Product_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Product");
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase postedFile)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                product.Image = "temp";
                if (ModelState.IsValid)
                {
                    var userinfo = (User)Session[Constants.USER_INFO];
                    string fullfilename = "";
                    if (postedFile != null)
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        string filename = Path.GetFileName(postedFile.FileName);
                        fullfilename = filename.Split('.')[0] + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ")." + filename.Split('.')[1];
                        //Luu duong dan cua file
                        string path = Path.Combine(Server.MapPath("~/Data/ImgAuthor"), fullfilename);
                        postedFile.SaveAs(path);

                        product.Image = fullfilename;
                    }
                    if (product.ProductStatus > 0)
                    {
                        if (!string.IsNullOrEmpty(product.ProductStatus.ToString()))
                        {
                            product.PromotionPrice = product.Price - product.Price * (product.ProductStatus / 100);
                        }
                    }

                    var cate = new ProductCategoryDao().GetDetail((long)product.CategoryID);
                    product.Code = cate.MetaTitle.ToString() + "_" + product.ID.ToString();
                    product.Image = fullfilename;
                    product.Status = true;
                    product.ModifiedBy = userinfo.UserName;
                    product.MetaTitle = Unicode.RemoveUnicode(product.Name).Replace(" ", "-").Replace("&", "va").Replace(":", "la").ToLower().ToString();
                    product.Tag = Unicode.RemoveUnicode(product.Name).ToLower().ToString();
                    //product.ModifiedBy = Session[Constants.USER_USERNAME].ToString();

                    var result = new ProductDao().Update(product);

                    if (result)
                    {
                        //Cập nhật lại hình sau khi update

                        //Begin (Tạo giá)
                        var pricemodel = new PriceDao();
                        //if (pricemodel.CheckByPrice(product.ID, product.Price, product.PromotionPrice) == false)
                        //{
                        pricemodel.SetStatusFalse(product.ID);
                        var price = new Price();
                        price.PriceValue = product.Price;
                        price.PromotionPrice = product.PromotionPrice;
                        price.Name = product.Name;
                        price.ProductID = product.ID;
                        price.CreatedBy = userinfo.UserName;

                        pricemodel.Insert(price);
                        //End (Tạo giá)

                        SetAlert("Thay đổi thông tin Product thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_Product_Edit", "Thay đổi thông tin Product thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        SetAlert("Thay đổi thông tin Product không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_Product_Edit", "Thay đổi thông tin Product không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {
                    new LogDao().SetLog("Admin_Product_Edit", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCT);
                    ViewBag.ListPublisher = new PublisherDao().ListAllPublisherActive();
                    ViewBag.ListAuthor = new AuthorDao().ListAllAuthorActive();
                    ViewBag.ListCategory = new ProductCategoryDao().ListAllProductCategoryActive();
                    return View("Edit", product);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Product_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Delete(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCT);
                var ProductModel = new ProductDao().GetDetail(id);
                ViewBag.ListPublisher = new PublisherDao().ListAllPublisherActive();
                ViewBag.ListAuthor = new AuthorDao().ListAllAuthorActive();
                ViewBag.ListCategory = new ProductCategoryDao().ListAllProductCategoryActive();
                new LogDao().SetLog("Admin_Product_Delete", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(ProductModel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Product_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Product");
        }
        [HttpPost]
        public ActionResult Delete(Product product)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                var pricemodel = new PriceDao().ListByProductID(product.ID);
                for (int i = 0; i < pricemodel.Count; i++)
                {
                    var model = new PriceDao().Remove(pricemodel[i]);
                }
                var result = new ProductDao().Remove(product);
                if (result)
                {
                    SetAlert("Xoá Product thành công", Constants.ALERTTYPE_SUCCESS);
                    new LogDao().SetLog("Admin_Product_Delete", "Xoá Product thành công", ((User)Session[Constants.USER_INFO]).ID);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Xoá Product không thành công", Constants.ALERTTYPE_ERROR);
                    new LogDao().SetLog("Admin_Product_Delete", "Xoá Product không thành công", ((User)Session[Constants.USER_INFO]).ID);
                    return RedirectToAction("Index", "Product");
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_Product_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Detail(long id)
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCT);
            var ProductModel = new ProductDao().GetDetail(id);
            ViewBag.ListPublisher = new PublisherDao().ListAllPublisherActive();
            ViewBag.ListAuthor = new AuthorDao().ListAllAuthorActive();
            ViewBag.ListCategory = new ProductCategoryDao().ListAllProductCategoryActive();
            new LogDao().SetLog("Admin_Product_Detail", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(ProductModel);
        }
        [HttpPost]
        public ActionResult ChangeStatus(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}