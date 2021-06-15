using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        EWBOK_DbContext db = null;
        public OrderDetailDao()
        {
            db = new EWBOK_DbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
        public List<OrderDetail> ListAllOrderDetail()
        {
            return db.OrderDetails.ToList();
        }
        public List<OrderDetail> ListOrderDetailById(long id)
        {
            return db.OrderDetails.Where(x => x.OrderID == id).ToList();
        }
    }
}
