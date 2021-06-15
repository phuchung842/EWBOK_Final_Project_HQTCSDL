using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class WishDao
    {
        EWBOK_DbContext db = null;
        public WishDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<Wish> ListAllWish(User user)
        {
            return db.Wishes.Where(x => x.UserID == user.ID).OrderByDescending(x => x.CreateDate).ToList();
        }
        public bool Insert(Wish wish)
        {
            try
            {
                db.Wishes.Add(wish);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(long productId,long userId)
        {
            try
            {
                var wish = db.Wishes.SingleOrDefault(x => x.ProductID == productId && x.UserID == userId);
                db.Wishes.Remove(wish);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool FindByUserIdAndProductID(long userId, long productId)
        {
            var result = db.Wishes.SingleOrDefault(x => x.ProductID == productId && x.UserID == userId);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
