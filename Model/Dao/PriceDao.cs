using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PriceDao
    {
        EWBOK_DbContext db = null;
        public PriceDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<Price> ListAllPrice()
        {
            return db.Prices.OrderBy(x => x.ProductID).ToList();
        }
        public List<Price> ListByProductID(long id)
        {
            return db.Prices.Where(x => x.ProductID == id).ToList();
        }
        public Price GetDetail(long id)
        {
            return db.Prices.Find(id);
        }
        public bool? CheckStatus(Price PriceEntity)
        {
            return db.Prices.Find(PriceEntity.ID).Status;
        }
        public long Insert(Price PriceEntity)
        {
            PriceEntity.CreateDate = DateTime.Now;
            PriceEntity.Status = true;
            db.Prices.Add(PriceEntity);
            db.SaveChanges();
            return PriceEntity.ID;
        }
        //public bool Update(Price PriceEntity)
        //{
        //    try
        //    {
        //        var price = db.Prices.Find(PriceEntity.ID);
        //        price.Status = PriceEntity.Status;

        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        public bool Remove(Price PriceEntity)
        {
            try
            {
                var Price = db.Prices.Find(PriceEntity.ID);
                db.Prices.Remove(Price);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Dùng cho set các giá ở id của product đó thành false hết
        public bool SetStatusFalse(long id)
        {
            try
            {
                var price = db.Prices.Where(x => x.ProductID == id).ToList();
                foreach (var item in price)
                {
                    item.Status = false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckByPrice(long id, decimal? pricevalue, decimal? promotionprice)
        {
            try
            {
                var price = db.Prices.Where(x => x.ProductID == id && x.PriceValue == pricevalue && x.PromotionPrice == promotionprice).ToList();
                if (price.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        //public Price FindByPrice(long id, decimal? pricevalue, decimal? promotionprice)
        //{
        //    return db.Prices.SingleOrDefault(x => x.ProductID == id && x.PriceValue == pricevalue && x.PromotionPrice == promotionprice);
        //}
    }
}
