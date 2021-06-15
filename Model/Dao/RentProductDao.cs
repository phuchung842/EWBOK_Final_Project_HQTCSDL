using Model.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class RentProductDao
    {
        EWBOK_DbContext db = null;
        public RentProductDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<RentProduct> ListRentProductByProID(long id)
        {
            return db.RentProducts.Where(x => x.Status == 1 && x.ProductID == id).ToList();
        }
        
    }
}
