using Model.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        EWBOK_DbContext db = null;
        public ProductCategoryDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<ProductCategory> ListAllProductCategory()
        {
            return db.ProductCategories.ToList();
        }
        public List<ProductCategory> ListAllProductCategoryActive()
        {
            return db.ProductCategories.Where(x => x.Status == true).ToList();
        }
        public ProductCategory GetDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public ProductCategory GetProductCategory()
        {
            return db.ProductCategories.FirstOrDefault();
        }
        public bool CheckByColor(string color)
        {
            return db.ProductCategories.Count(x => x.Color == color) > 0;
        }
        public long Insert(ProductCategory productCategoryEntity)
        {
            productCategoryEntity.CreateDate = DateTime.Now;
            db.ProductCategories.Add(productCategoryEntity);
            db.SaveChanges();
            return productCategoryEntity.ID;
        }

        public bool Update(ProductCategory productCategoryEntity)
        {
            try
            {
                var procate = db.ProductCategories.Find(productCategoryEntity.ID);
                if (!string.IsNullOrEmpty(productCategoryEntity.Name))
                {
                    procate.Name = productCategoryEntity.Name;
                }
                if(!string.IsNullOrEmpty(productCategoryEntity.MetaTitle))
                {
                    procate.MetaTitle = productCategoryEntity.MetaTitle;
                }
                procate.ModifiedBy = productCategoryEntity.ModifiedBy;
                procate.ModifiedDate = DateTime.Now;
                procate.Status = productCategoryEntity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(ProductCategory productCategoryEntity)
        {
            try
            {
                var procate = db.ProductCategories.Find(productCategoryEntity.ID);
                db.ProductCategories.Remove(procate);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var productcate = db.ProductCategories.Find(id);
            productcate.Status = !productcate.Status;
            db.SaveChanges();
            return productcate.Status;
        }
    }
}
