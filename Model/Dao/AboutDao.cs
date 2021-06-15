using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AboutDao
    {
        EWBOK_DbContext db = null;
        public AboutDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<About> ListAllAbout()
        {
            return db.Abouts.ToList();
        }
        public About GetDetail(long id)
        {
            return db.Abouts.Find(id);
        }
        public bool Update(About AboutEntity)
        {
            try
            {
                var about = db.Abouts.Find(AboutEntity.ID);
                about.Address = AboutEntity.Address;
                about.Decription = AboutEntity.Decription;
                about.Detail = AboutEntity.Detail;
                about.Email = AboutEntity.Email;
                about.ModifiedBy = AboutEntity.ModifiedBy;
                about.ModifiedDate = DateTime.Now;
                about.Name = AboutEntity.Name;
                about.Phone = AboutEntity.Phone;
                about.Status = AboutEntity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
