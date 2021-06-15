using EWBOK_Final_Project.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EWBOK_Final_Project.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCTCATEGORY);
            var productcatemodel = new ProductCategoryDao().ListAllProductCategory();
            new LogDao().SetLog("Admin_ProductCategory_Index", null, ((User)Session[Constants.USER_INFO]).ID);
            return View(productcatemodel);
        }

        public ActionResult Create()
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCTCATEGORY);
                new LogDao().SetLog("Admin_ProductCategory_Create", null, ((User)Session[Constants.USER_INFO]).ID);
                return View();
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_ProductCategory_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "ProductCategory");
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (ModelState.IsValid)
                {
                    var userinfo = (User)Session[Constants.USER_INFO];
                    productCategory.MetaTitle = Unicode.RemoveUnicode(productCategory.Name).Replace(" ", "-").ToLower().ToString();
                    productCategory.Status = true;
                    productCategory.CreatedBy = userinfo.UserName;

                    //tạo màu cho thể loại
                    Random r = new Random();
                    Color color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                    string hexcode = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
                    int lenght = 1;
                    for (int i = 0; i < lenght; i++)
                    {
                        var checkcolor = new ProductCategoryDao().CheckByColor(hexcode);
                        if (checkcolor)
                        {
                            lenght++;
                            color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                            hexcode = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
                        }
                        else
                        {
                            break;
                        }
                    }
                    //hết tạo màu cho thể loại
                    productCategory.Color = hexcode;
                    //productCategory.CreatedBy = Session[Constants.USER_USERNAME].ToString();

                    long id = new ProductCategoryDao().Insert(productCategory);
                    if (id > 0)
                    {
                        SetAlert("Tạo Product Category thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_ProductCategory_Create", "Tạo Product Category thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "ProductCategory");
                    }
                    else
                    {
                        SetAlert("Tạo ProductCategory không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_ProductCategory_Create", "Tạo ProductCategory không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "ProductCategory");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    new LogDao().SetLog("Admin_ProductCategory_Create", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Create",productCategory);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_ProductCategory_Create", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "ProductCategory");
        }

        public ActionResult Edit(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCTCATEGORY);
                var productcatemodel = new ProductCategoryDao().GetDetail(id);
                new LogDao().SetLog("Admin_ProductCategory_Edit", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(productcatemodel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_ProductCategory_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "ProductCategory");
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                if (ModelState.IsValid)
                {
                    var userinfo = (User)Session[Constants.USER_INFO];
                    productCategory.MetaTitle = Unicode.RemoveUnicode(productCategory.Name).Replace(" ", "-").ToLower().ToString();
                    productCategory.ModifiedBy = userinfo.UserName;
                    //productCategory.ModifiedBy = Session[Constants.USER_USERNAME].ToString();

                    var result = new ProductCategoryDao().Update(productCategory);

                    if (result)
                    {
                        //Cập nhật lại hình sau khi update
                        SetAlert("Thay đổi thông tin Product Category thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_ProductCategory_Edit", "Thay đổi thông tin Product Category thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "ProductCategory");
                    }
                    else
                    {
                        SetAlert("Thay đổi thông tin Product Category không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_ProductCategory_Edit", "Thay đổi thông tin Product Category không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "ProductCategory");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không lệ");
                    new LogDao().SetLog("Admin_ProductCategory_Edit", "Dữ liệu không hợp lệ", ((User)Session[Constants.USER_INFO]).ID);
                    return View("Edit", productCategory);
                }
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_ProductCategory_Edit", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "ProductCategory");
        }

        public ActionResult Delete(long id)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCTCATEGORY);
                var productcatemodel = new ProductCategoryDao().GetDetail(id);
                new LogDao().SetLog("Admin_ProductCategory_Delete", null, ((User)Session[Constants.USER_INFO]).ID);
                return View(productcatemodel);
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_ProductCategory_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "ProductCategory");
        }
        [HttpPost]
        public ActionResult Delete(ProductCategory productCategory)
        {
            if (((User)Session[Constants.USER_INFO]).GroupID == Constants.GROUP_ADMIM)
            {
                var setnull = new ProductDao().SetNullProductCategory(productCategory.ID);
                if (setnull)
                {
                    var result = new ProductCategoryDao().Remove(productCategory);
                    if (result)
                    {
                        SetAlert("Xoá Product Category thành công", Constants.ALERTTYPE_SUCCESS);
                        new LogDao().SetLog("Admin_ProductCategory_Delete", "Xoá Product Category thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "ProductCategory");
                    }
                    else
                    {
                        SetAlert("Xoá Product Category không thành công", Constants.ALERTTYPE_ERROR);
                        new LogDao().SetLog("Admin_ProductCategory_Delete", "Xoá Product Category không thành công", ((User)Session[Constants.USER_INFO]).ID);
                        return RedirectToAction("Index", "ProductCategory");
                    }
                }
                new LogDao().SetLog("Admin_ProductCategory_Delete", "Lỗi set null", ((User)Session[Constants.USER_INFO]).ID);
                return View("Index");
            }
            SetAlert("Tài khoản của bạn không có quyền", Constants.ALERTTYPE_ERROR);
            new LogDao().SetLog("Admin_ProductCategory_Delete", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return RedirectToAction("Index", "ProductCategory");
        }

        public ActionResult Detail(long id)
        {
            SetActiveSlideBar(Constants.SLIDEBAR_AD_PRODUCTCATEGORY);
            var productcatemodel = new ProductCategoryDao().GetDetail(id);
            new LogDao().SetLog("Admin_ProductCategory_Detail", "Tài khoản của bạn không có quyền", ((User)Session[Constants.USER_INFO]).ID);
            return View(productcatemodel);
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductCategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}