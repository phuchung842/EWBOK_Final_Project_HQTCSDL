using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        EWBOK_DbContext db = null;
        public ProductDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<Product> ListAllProduct()
        {
            return db.Products.OrderBy(x => x.Name).ToList();
        }
        public List<Product> ListProductByCategory(long id)
        {
            return db.Products.Where(x => x.CategoryID == id).OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Product> ListProductbyAuthor(long id)
        {
            return db.Products.Where(x => x.AuthorID == id).OrderByDescending(x => x.CreateDate).ToList();
        }

        public Product GetDetail(long id)
        {
            return db.Products.Find(id);
        }
        public long Insert(Product ProductEntity)
        {
            ProductEntity.CreateDate = DateTime.Now;
            //ProductEntity.Code = ProductEntity.ProductCategory.MetaTitle + ProductEntity.ID.ToString();
            ProductEntity.IncludeVAT = true;
            ProductEntity.ViewCount = 0;
            ProductEntity.WishCount = 0;
            ProductEntity.SellerCount = 0;
            db.Products.Add(ProductEntity);
            db.SaveChanges();
            return ProductEntity.ID;
        }
        public bool Update(Product ProductEntity)
        {
            //try
            //{
            var product = db.Products.Find(ProductEntity.ID);
            if (!string.IsNullOrEmpty(ProductEntity.Image))
            {
                product.Image = product.Image;
            }
            if (!string.IsNullOrEmpty(ProductEntity.Name))
            {
                product.Name = ProductEntity.Name;
            }
            if (!string.IsNullOrEmpty(ProductEntity.Price.ToString()))
            {
                product.Price = ProductEntity.Price;
            }
            if (!string.IsNullOrEmpty(ProductEntity.PromotionPrice.ToString()))
            {
                product.PromotionPrice = ProductEntity.PromotionPrice;
            }
            product.Code = ProductEntity.Code;
            product.IncludeVAT = ProductEntity.IncludeVAT;
            product.Warranty = ProductEntity.Warranty;
            product.ProductStatus = ProductEntity.ProductStatus;
            product.CategoryID = ProductEntity.CategoryID;
            product.AuthorID = ProductEntity.AuthorID;
            product.Decription = ProductEntity.Decription;
            product.Detail = ProductEntity.Detail;
            product.Form = ProductEntity.Form;
            product.Pages = ProductEntity.Pages;
            if (!string.IsNullOrEmpty(ProductEntity.PublishYear.ToString()))
            {
                product.PublishYear = ProductEntity.PublishYear;
            }
            product.PublisherID = ProductEntity.PublisherID;
            product.Quantity = ProductEntity.Quantity;
            product.Size = ProductEntity.Size;
            product.ModifiedBy = ProductEntity.ModifiedBy;
            product.ModifiedDate = DateTime.Now;
            product.Status = ProductEntity.Status;
            product.ViewCount = ProductEntity.ViewCount;
            product.WishCount = ProductEntity.WishCount;
            product.SellerCount = ProductEntity.SellerCount;
            product.MetaTitle = ProductEntity.MetaTitle;
            product.Tag = ProductEntity.Tag;
            db.SaveChanges();
            return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

        public bool Remove(Product ProductEntity)
        {
            try
            {
                var Product = db.Products.Find(ProductEntity.ID);
                db.Products.Remove(Product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        

        public bool? ChangeStatus(long id)
        {
            var product = db.Products.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

        public bool SetNullAuthor(long id)
        {
            try
            {
                var product = db.Products.Where(x => x.AuthorID == id).ToList();
                for (int i = 0; i < product.Count; i++)
                {
                    product[i].AuthorID = null;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetNullProductCategory(long id)
        {
            try
            {
                var product = db.Products.Where(x => x.CategoryID == id).ToList();
                for (int i = 0; i < product.Count; i++)
                {
                    product[i].CategoryID = null;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetNullPublisher(long id)
        {
            try
            {
                var product = db.Products.Where(x => x.PublisherID == id).ToList();
                for (int i = 0; i < product.Count; i++)
                {
                    product[i].PublisherID = null;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Product> ListProductByNewTemp(int? quantity = null)
        {
            if (quantity.HasValue)
            {
                if(db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true).ToList().Count()>quantity)
                {
                    return db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true).Take(quantity.Value).ToList();
                }
            }
            return db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true).ToList();
        }
        public List<Product> ListProductByNew(int? quantity = null)
        {
            DateTime limit = DateTime.Now.AddMonths(-1);
            if (quantity.HasValue)
            {
                if (db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true && x.CreateDate >= limit).ToList().Count() > quantity)
                {
                    return db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true && x.CreateDate >= limit).Take(quantity.Value).ToList();
                }
                return db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true && x.CreateDate >= limit).ToList();
            }
            return db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true && x.CreateDate >= limit).ToList();
        }
        public List<Product> ListProductByViewCount(int? quantity = null)
        {
            if (quantity.HasValue)
            {
                if (db.Products.OrderByDescending(x => x.ViewCount).Where(x => x.Status == true).ToList().Count() > quantity)
                {
                    return db.Products.OrderByDescending(x => x.ViewCount).Where(x => x.Status == true).Take(quantity.Value).ToList();
                }
                return db.Products.OrderByDescending(x => x.ViewCount).Where(x => x.Status == true).ToList();
            }
            return db.Products.OrderByDescending(x => x.ViewCount).Where(x => x.Status == true).ToList();
        }
        public List<Product> ListProductByDisCount(int? quantity = null)
        {
            if (quantity.HasValue)
            {
                if (db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true && x.ProductStatus > 0).ToList().Count() > quantity)
                {
                    return db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true && x.ProductStatus > 0).Take(quantity.Value).ToList();
                }
                return db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true && x.ProductStatus > 0).ToList();
            }
            return db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.Status == true && x.ProductStatus > 0).ToList();
        }
        public List<Product> ListProductByBestSeller(int? quantity = null)
        {
            if (quantity.HasValue)
            {
                if (db.Products.OrderByDescending(x => x.SellerCount).Where(x => x.Status == true).ToList().Count() > quantity)
                {
                    return db.Products.OrderByDescending(x => x.SellerCount).Where(x => x.Status == true).Take(quantity.Value).ToList();
                }
                return db.Products.OrderByDescending(x => x.SellerCount).Where(x => x.Status == true).ToList();
            }
            return db.Products.OrderByDescending(x => x.SellerCount).Where(x => x.Status == true).ToList();
        }
        public List<Product> LisProductByWishCount(int? quantity = null)
        {
            if (quantity.HasValue)
            {
                if (db.Products.OrderByDescending(x => x.WishCount).Where(x => x.Status == true).ToList().Count() > quantity)
                {
                    return db.Products.OrderByDescending(x => x.WishCount).Where(x => x.Status == true).Take(quantity.Value).ToList();
                }
                return db.Products.OrderByDescending(x => x.WishCount).Where(x => x.Status == true).ToList();
            }
            return db.Products.OrderByDescending(x => x.WishCount).Where(x => x.Status == true).ToList();
        }

        public List<Product> ListRelatedProduct(Product product)
        {
            return db.Products.Where(x => x.CategoryID == product.CategoryID && x.ID != product.ID).OrderByDescending(x => x.CreateDate).ToList();
        }

        public List<Product> ListProductBySearching(string searchkey)
        {
            object[] sqlparams =
            {
                new SqlParameter("@searchkey",searchkey)
            };
            var list = db.Products.SqlQuery("[dbo].[sp_Searching] @searchkey", sqlparams).ToList();
            return list;
        }

        //kết hợp với  ListAuthorBySearchKey
        //public List<Product> ListProductByListAuthor(List<Author> authors)
        //{
        //    List<Product> products = new List<Product>();
        //    for (int i = 0; i < authors.Count; i++)
        //    {
        //        long? id = authors[i].ID;
        //        products = products.Concat(db.Products.Where(x => x.AuthorID == id).ToList()).ToList();
        //    }
        //    return products;
        //}
        ////kết hợp với ListPublisherBySearchKey
        //public List<Product> ListProductByListPublisher(List<Publisher> publishers)
        //{
        //    List<Product> products = new List<Product>();
        //    for (int i = 0; i < publishers.Count; i++)
        //    {
        //        long? id = publishers[i].ID;
        //        products = products.Concat(db.Products.Where(x => x.PublisherID == id).ToList()).ToList();
        //    }
        //    return products;
        //}
        //public List<Product> ListProductBySearching(string searchkey)
        //{
        //    return db.Products.Where(x => x.Status == true && (x.Name.ToUpper().Contains(searchkey.ToUpper()) || x.Tag.ToUpper().Contains(searchkey.ToUpper()))).ToList();
        //}
        public List<Product> ListAllProductActive()
        {
            return db.Products.Where(x => x.Status == true).ToList();
        }
    }
}
