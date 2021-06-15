using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PublisherDao
    {
        EWBOK_DbContext db = null;
        public PublisherDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<Publisher> ListAllPublisher()
        {
            return db.Publishers.ToList();
        }
        public List<Publisher> ListAllPublisherActive()
        {
            return db.Publishers.Where(x => x.Status == true).ToList();
        }
        public Publisher GetDetail(long id)
        {
            return db.Publishers.Find(id);
        }
        public long Insert(Publisher PublisherEntity)
        {
            PublisherEntity.CreateDate = DateTime.Now;
            db.Publishers.Add(PublisherEntity);
            db.SaveChanges();
            return PublisherEntity.ID;
        }
        public bool Update(Publisher PublisherEntity)
        {
            try
            {
                var publisher = db.Publishers.Find(PublisherEntity.ID);
                if (!string.IsNullOrEmpty(PublisherEntity.Name))
                {
                    publisher.Name = PublisherEntity.Name;
                }
                if(!string.IsNullOrEmpty(PublisherEntity.Website))
                {
                    publisher.Website = PublisherEntity.Website;
                }
                if(!string.IsNullOrEmpty(PublisherEntity.Email))
                {
                    publisher.Email = PublisherEntity.Email;
                }
                if(!string.IsNullOrEmpty(PublisherEntity.Phone))
                {
                    publisher.Phone = PublisherEntity.Phone;
                }
                if(!string.IsNullOrEmpty(PublisherEntity.Address))
                {
                    publisher.Address = PublisherEntity.Address;
                }
                if(!string.IsNullOrEmpty(PublisherEntity.Fax))
                {
                    publisher.Fax = PublisherEntity.Fax;
                }
                publisher.ModifiedBy = PublisherEntity.ModifiedBy;
                publisher.ModifiedDate = DateTime.Now;
                publisher.Status = PublisherEntity.Status;
                publisher.MetaTitle = PublisherEntity.MetaTitle;
                publisher.Tag = PublisherEntity.Tag;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Remove(Publisher PublisherEntity)
        {
            try
            {
                var Publisher = db.Publishers.Find(PublisherEntity.ID);
                db.Publishers.Remove(Publisher);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var publisher = db.Publishers.Find(id);
            publisher.Status = !publisher.Status;
            db.SaveChanges();
            return publisher.Status;
        }

        //kết hợp với ListProductByListPublisher
        public List<Publisher> ListPublisherBySearchKey(string searchkey)
        {
            return db.Publishers.Where(x => x.Status == true && (x.Name.ToUpper().Contains(searchkey.ToUpper())||x.Tag.ToUpper().Contains(searchkey.ToUpper()))).ToList();
        }
    }
}
